namespace EmpManagementSystem.Services
{
    public class FileUpload : IFileUpload
    {
        public string? FileName { get; set; }

        public async Task<bool> UploadFile(IFormFile file)
        {
            string path = "";
            try
            {
                if(file.Length> 0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "wwwroot/images"));
                    using (var filestream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(filestream);
                        this.FileName = file.FileName;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch(Exception ex)
            {
                throw new Exception("Filecopy failed", ex);
            }
        }
    }
}
