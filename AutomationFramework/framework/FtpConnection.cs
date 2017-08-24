using System;
using WinSCP;

namespace AutomationFramework.framework
{
    public class FtpConnection
    {
        private Session session = new Session();
        private SessionOptions sessionOptions = new SessionOptions();

        public FtpConnection()
        {
            Initialize();
        }

        private void Initialize()
        {
            sessionOptions.Protocol = Protocol.Sftp;
            sessionOptions.HostName = Env.config.ftp_host_name;
            sessionOptions.UserName = Env.config.ftp_user_id;
            sessionOptions.Password = Env.config.ftp_password;
            sessionOptions.SshHostKeyFingerprint = Env.config.ssh_hostkey_fingerprint;
        }

        private bool OpenSession()
        {
            try
            {
                session.Open(sessionOptions);
                return true;
            }
            catch (SessionException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private bool CloseSession()
        {
            try
            {
                session.Close();
                return true;
            }
            catch (SessionException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void UploadFiles(string directory, string uploadPath, bool isRemove)
        {
            if (this.OpenSession() == true)
            {
                TransferOptions options = new TransferOptions();
                options.TransferMode = TransferMode.Binary;

                TransferOperationResult result;
                result = session.PutFiles(directory, uploadPath, isRemove, options);

                result.Check();

                foreach (TransferEventArgs transfer in result.Transfers)
                {
                    Console.WriteLine("Upload of {0} succeeded", transfer.FileName);
                }

                this.CloseSession();
            }
        }

        public void RemoveFiles(string fileMask)
        {
            if (this.OpenSession() == true)
            {
                RemovalOperationResult result;
                result = session.RemoveFiles(fileMask);

                result.Check();

                foreach (RemovalEventArgs removal in result.Removals)
                {
                    Console.WriteLine("Removal of {0} succeeded", removal.FileName);
                }

                this.CloseSession();
            }
        }
    }
}