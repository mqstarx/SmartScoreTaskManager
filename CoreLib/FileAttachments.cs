using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    [Serializable]
    public class FileAttachments
    {
        private const int MAX_LEN = 26214400;
        private List<UserFile> m_Files;
        private FileAttachmentsInfo m_AttachInfo;
       

        public FileAttachments()
        {
            m_Files = new List<UserFile>();
            m_AttachInfo = new FileAttachmentsInfo();
        }
        public UserFile this[int index]
        {
            get
            {  return m_Files[index]; }
        }
        private int GetAllFileLen()
        {
            int result = 0;
            if (m_Files == null)
                return 0;
            foreach(UserFile f in m_Files)
            {
                result += f.FileLength;
            }
            return result;
        }
        /// <summary>
        /// добавляет файл в коллекцию
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool AddFile(string path)
        {
            FileInfo fi = new FileInfo(path);
           
            if (GetAllFileLen() + fi.Length > MAX_LEN)
                return false;
            else
            {
                if (m_Files == null)
                    m_Files = new List<UserFile>();
                UserFile _file = new UserFile(path);
                m_Files.Add(_file);
                m_AttachInfo.FilesInfo.Add(_file.UserFileInfo);
                return true;
            }
        }
        public bool AddFile(UserFile file)
        {
           
            if (GetAllFileLen() + file.FileLength > MAX_LEN)
                return false;
            else
            {
                if (m_Files == null)
                    m_Files = new List<UserFile>();
                m_Files.Add(file);
                m_AttachInfo.FilesInfo.Add(file.UserFileInfo);
                return true;
            }
        }
        public bool AddFiles(string[] paths)
        {
            if (paths != null)
            {
                int sum_file_len = GetAllFileLen();
                for (int i = 0; i < paths.Length; i++)
                {
                    FileInfo fi = new FileInfo(paths[i]);
                    if (sum_file_len + fi.Length > MAX_LEN)
                        return false;

                    sum_file_len += (int)fi.Length;
                }
                for (int i = 0; i < paths.Length; i++)
                {
                    AddFile(paths[i]);
                }
                return true;

            }
            else
                return false;
        }
        public bool AddFiles(UserFile[] files)
        {
            if (files != null)
            {
                int sum_file_len = 0;
                for (int i = 0; i < files.Length; i++)
                {
                   
                    if (sum_file_len + files[i].FileLength > MAX_LEN)
                        return false;
                       sum_file_len += files[i].FileLength;
                }
                for (int i = 0; i < files.Length; i++)
                {
                    AddFile(files[i]);
                }
                return true;

            }
            else
                return false;
        }

        public bool RemoveFile(string filename)
        {
            if (m_Files != null)
            {
                bool is_delete_file=false;
                bool is_delete_fileinfo=false;
                foreach (UserFile uf in m_Files)
                {
                    if (uf.FileName == filename)
                    {
                        m_Files.Remove(uf);
                        is_delete_file = true;
                        break;        
                    }
                }
                foreach (UserFileInfo uf in m_AttachInfo.FilesInfo)
                {
                    if (uf.FileName == filename)
                    {
                        m_AttachInfo.FilesInfo.Remove(uf);
                        is_delete_fileinfo = true;
                        break;
                    }
                }
                if (is_delete_file && is_delete_fileinfo)
                    return true;

            }
            return false;
        }
       /* public bool RemoveFiles(string[] filenames)
        {
            if(m_Files!=null)
            {
                for(int i=0;i<filenames.Length;i++)
                {

                }
            }

        }*/

        public int Count
        {
            get {

                return m_Files.Count;
            }
        }

        public FileAttachmentsInfo AttachInfo
        {
            get
            {
                return m_AttachInfo;
            }

           
        }

        
    }
}
