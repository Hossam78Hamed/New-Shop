
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Product;
using Shop.Core.interfaces;
using System.IO;

namespace Shop.Core.Services.PhotoServices
{
    public class PhotoService : IPhotoService
    {
        //IFileProvider
        private readonly IFileProvider fileProvider;
        private readonly IPhotoRepository photoRepository;

        public PhotoService(IFileProvider _fileProvider,IPhotoRepository _photoRepository) {
            fileProvider = _fileProvider;
            photoRepository = _photoRepository;
        }
        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            List<string> savedImagePaths = new List<string>();

            // 1) التحقق من أن هناك ملفات مرفوعة
            if (files == null || files.Count == 0)
                return savedImagePaths;

            // 2) إنشاء مسار المجلد
            string imageDirectory = Path.Combine("wwwroot", "Images", src);

            // 3) إذا لم يكن المجلد موجودًا، يتم إنشاؤه
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    // 4) التحقق من نوع الملف (صور فقط)
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                    var extension = Path.GetExtension(file.FileName).ToLower();

                    if (!allowedExtensions.Contains(extension))
                        continue; // تخطي الملفات غير المسموح بها

                    // 5) توليد اسم فريد للملف لتجنب التكرار
                    var uniqueFileName = $"{Guid.NewGuid()}{extension}";

                    // 6) رابط الصورة للعرض
                    var imageSrc = $"/Images/{src}/{uniqueFileName}";

                    // 7) المسار الفعلي على السيرفر
                    var filePath = Path.Combine(imageDirectory, uniqueFileName);

                    // 8) الحفظ على السيرفر مع استخدام using للتأكد من غلق الـ Stream
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    savedImagePaths.Add(imageSrc);
                }
            }

            return savedImagePaths;
        }

        public async Task<List<Photo>> AddRangeImageAsync(List<Photo> Photos)
        {
            //photoRepository
           var photosDB =await photoRepository.AddRangeImageInDBAsync(Photos) ;
            return photosDB;
        }
        public async Task<Photo> HeardDeleteAsync(int Id) { 
        
        Photo? photo= await photoRepository.GetByIdAsync(Id);

            if (photo != null) {
                photoRepository.DeleteAsync(Id);

                return photo;
            }
            return null;



        }
        public async Task<bool> DeleteImageAsync(string src)
            {
                if (string.IsNullOrWhiteSpace(src))
               ///     throw new ArgumentException("المسار فارغ أو غير صحيح.", nameof(src));
            {
               
                return false;
            }
                var info = fileProvider.GetFileInfo(src);
                var root = info.PhysicalPath;
           // Console.WriteLine($"root { root}");

            //

            //string normalized = root.Replace(@"\\", @"\");

            // ✅ إدخال Images بعد wwwroot
            string normalized = root.Replace(@"\wwwroot\", @"\wwwroot\Images\");
           // Console.WriteLine($"normalized {normalized}");
            //

           // string normalized2 = Path.GetFullPath(root);

            // ✅ إدخال "Images" بعد wwwroot
           // normalized2 = normalized2.Replace(@"\wwwroot\", @"\wwwroot\Images\");

            //Console.WriteLine($"normalized2 {normalized2}");


            if (string.IsNullOrEmpty(normalized) )//|| !File.Exists(normalized2))
            //if (string.IsNullOrEmpty(root) )

            //throw new FileNotFoundException("الملف غير موجود.", src);
            {
                // return "The file does not exist";
                return false;
            }
            Directory.Delete(normalized, true);
           // await Task.Run(() => File.Delete(normalized2));

           // File.Delete(root);
            return true;    
        }
      public  async Task<List<Photo>> GetAllAsync() { 
        var photos=(await photoRepository.GetAllAsync()).ToList();
            
            return photos;


        }
        
        
        


    }
}
