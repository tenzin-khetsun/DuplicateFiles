namespace DupFiles{
    class DuplicateFiles{
        static string GetFileHash(string filePath)
    {
        using (var md5 = System.Security.Cryptography.MD5.Create())
        {
            using (var stream = File.OpenRead(filePath))
            {
                byte[] hashBytes = md5.ComputeHash(stream);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
        static void Main(){
            Console.WriteLine("Enter the directory path");
            string path = Console.ReadLine();
            if(Directory.Exists(path)){
                try{
                     Dictionary<string, List<string>> fileHashes = new Dictionary<string, List<string>>();
                List<string> duplicateFiles = new List<string>();

                string[] allFiles = Directory.GetFiles(path, "*", SearchOption.AllDirectories);

                foreach(var filePath in allFiles){
                    string fileHash = GetFileHash(filePath);
                    if(!fileHashes.ContainsKey(fileHash)){
                        fileHashes[fileHash] = new List<string>();
                    }
                    else{
                        duplicateFiles.Add(filePath);
                    }
                    fileHashes[fileHash].Add(filePath);
                }
                if(duplicateFiles.Count==0){
                    Console.WriteLine("There are no duplicate files");
                    return;
                }
                Console.WriteLine("Duplicate Files : ");
                foreach(string filePath in duplicateFiles)
                {
                    Console.WriteLine(filePath);
                    File.Delete(filePath);
                }
                Console.WriteLine("Duplice have been deleted");
                
                }
                catch(Exception e){
                    Console.WriteLine(e);
                }

               
            }

        }
}
}
