using Microsoft.ML.OnnxRuntime;          
using Microsoft.ML.OnnxRuntime.Tensors;  
using System.IO;                         
using System.Collections.Generic;
using System.Linq;

namespace Estatia.Services
{
    public class PricePredictionService
    {
        private readonly InferenceSession _session;

        public PricePredictionService(IWebHostEnvironment env)
        {
            
            string modelPath = Path.Combine(env.ContentRootPath, "estatia_model.onnx");
            
            
            _session = new InferenceSession(modelPath);
        }

        public float PredictPrice(float sizeSqFt, float cityCode, float typeCode)
        {
          
            var inputData = new float[] { sizeSqFt, cityCode, typeCode };
            var inputTensor = new DenseTensor<float>(inputData, new[] { 1, 3 });

           
            string inputName = _session.InputMetadata.Keys.First(); 
    
            var inputs = new List<NamedOnnxValue>
            {
               
                NamedOnnxValue.CreateFromTensor(inputName, inputTensor)
            };
          

            using (var results = _session.Run(inputs))
            {
                var resultTensor = results.First().AsTensor<float>();
                return resultTensor.First();
            }
        }
    }
}