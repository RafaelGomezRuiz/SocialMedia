using Microsoft.AspNetCore.Http;
using SocialMedia.Core.Application.Enums;

namespace SocialMedia.Core.Application.Helpers
{
    public static class UploadFile
    {
        public static string Upload(IFormFile file, string id,UploadFileTypes type, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode && file == null)
            {
                return imagePath;
            }
            string basePath=string.Empty;

            if (type.ToString()=="PROFILEPHOTO")
            {
                 basePath = $"/Images/Users/{id}";
            }
            else if (type.ToString()=="POSTFILE")
            {
                 basePath = $"/Images/Posts/{id}";
            }
            //FormatException correcta de combinar dos rutas
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //Create folder if not exists
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //GetHashCode file path
            Guid guid = Guid.NewGuid(); //NombreUnico
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);
            //subirlo
            //FileStream un string para manipular archivos
            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                //Este archvivo que el usuairo subio se copiara en el fileNameWithPath, que es el folder fisico que le enidque 
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split('/');
                string oldImageName = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImageName);
                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            //retornar la url que guardaremos en la imagen
            return $"{basePath}/{fileName}";
        }
    }
}
