using System.Text;
using DoctorTrainer.DTO.Metadata;
using DoctorTrainer.Entity;
using DoctorTrainer.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DoctorTrainer.Service;

public class ImageAnalysisService
{
    private readonly IConfiguration _configuration;
    private readonly ImageDataRepository _imageDataRepository;

    private readonly HttpClient _http;
    private List<ImageData> _inTrainingCache;

    public ImageAnalysisService(IConfiguration configuration, ImageDataRepository imageDataRepository)
    {
        _configuration = configuration;
        _imageDataRepository = imageDataRepository;
        _http = new HttpClient();
        _inTrainingCache = new List<ImageData>();
    }

    public ImageAnalysisService(List<ImageData> inTrainingCache)
    {
        _http = new HttpClient();
        _inTrainingCache = inTrainingCache;
    }

    public void ExportImageMetadata()
    {
        List<ImageData> imagesData = _imageDataRepository.FindAll();
        imagesData.ForEach(d => _inTrainingCache.Add(d));
        ImageDataExportCollectionDto collectionDto = ImageDataExportCollectionDto.EntityToDto(imagesData);

        string json = JsonConvert.SerializeObject(collectionDto);
        string path = $"{_configuration["BaseFilePath"]}{_configuration["MetadataFileName"]}";
        File.WriteAllText(path, json);
    }

    public bool IsInTraining(ImageData data)
    {
        return _inTrainingCache.Contains(data);
    }

    public bool IsInTraining(string imgId)
    {
        return _inTrainingCache.First(d => d.ImgId.Equals(imgId)) != null;
    }

    public async Task<HttpResponseMessage> TrainModel()
    {
        HttpResponseMessage response = await _http.PostAsync(_configuration["AIBaseUrl"] + "/train", null);
        _inTrainingCache.Clear();
        return response;
    }

    // returns marked region from AI service
    public async Task<ImageDataPredictionResponse?> Analyze(string imageId)
    {
        ImageData data = _imageDataRepository.FindByImageId(imageId);
        if (IsInTraining(data))
        {
            return null;
        }

        ImageDataPredictionRequest request = ImageDataPredictionRequest.EntityToDto(data);

        StringContent content = new StringContent(JsonConvert.SerializeObject(request, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        }), Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await _http.PostAsync(_configuration["AIBaseUrl"] + "/predict", content);
        string responseData = await response.Content.ReadAsStringAsync();
        ImageDataPredictionResponse? responseDto = JsonConvert.DeserializeObject<ImageDataPredictionResponse>(responseData);
        return responseDto;
    }
    
}