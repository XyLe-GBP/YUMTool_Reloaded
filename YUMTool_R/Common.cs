using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YUMTool_R
{
    /// <summary>
    /// 変数用クラス
    /// </summary>
    class Common
    {
        /// <summary>
        /// 'YUMFILE_1.BIN' (パーフェクトサン) 純正アーカイブのファイルサイズ [読み取り専用]
        /// </summary>
        public static readonly uint yum1_original_size = 0x33A91000;
        /// <summary>
        /// 'YUMFILE_2.BIN' (ワンダリングスター) 純正アーカイブのファイルサイズ [読み取り専用]
        /// </summary>
        public static readonly uint yum2_original_size = 0x342AB800;
        /// <summary>
        /// 'YUMFILE_3.BIN' (ミッシングムーン) 純正アーカイブのファイルサイズ [読み取り専用]
        /// </summary>
        public static readonly uint yum3_original_size = 0x366E7000;

        /// <summary>
        /// 'YUMFILE_1.BIN' (パーフェクトサン) 再構築後のアーカイブのファイルサイズ [読み取り専用]
        /// </summary>
        public static readonly uint yum1_repack_size = 0x440A7000;

        /// <summary>
        /// 'YUMFILE_1.BIN' (パーフェクトサン) 再構築後のアーカイブのファイルサイズ (圧縮処理を行わない) [読み取り専用]
        /// </summary>
        public static readonly uint yum1_repack_nc_size = 0x4CB32000;

        /// <summary>
        /// 'YUMFILE_2.BIN' (ワンダリングスター) 再構築後のアーカイブのファイルサイズ [読み取り専用]
        /// </summary>
        public static readonly uint yum2_repack_size = 0x4433F800;

        /// <summary>
        /// 'YUMFILE_2.BIN' (ワンダリングスター) 再構築後のアーカイブのファイルサイズ (圧縮処理を行わない) [読み取り専用]
        /// </summary>
        public static readonly uint yum2_repack_nc_size = 0x4DDB1000;

        /// <summary>
        /// 'YUMFILE_3.BIN' (ミッシングムーン) 再構築後のアーカイブのファイルサイズ [読み取り専用]
        /// </summary>
        public static readonly uint yum3_repack_size = 0x46AFB000;

        /// <summary>
        /// 'YUMFILE_3.BIN' (ミッシングムーン) 再構築後のアーカイブのファイルサイズ (圧縮処理を行わない) [読み取り専用]
        /// </summary>
        public static readonly uint yum3_repack_nc_size = 0x51038800;

        /// <summary>
        /// 'YUMFILE_1.BIN' (パーフェクトサン) アーカイブ展開後の全ファイル数 [読み取り専用]
        /// </summary>
        public static readonly uint yum1_progress_max = 0x62EA + 1;
        /// <summary>
        /// 'YUMFILE_2.BIN' (ワンダリングスター) アーカイブ展開後の全ファイル数 [読み取り専用]
        /// </summary>
        public static readonly uint yum2_progress_max = 0x659B + 1;
        /// <summary>
        /// 'YUMFILE_3.BIN' (ミッシングムーン) アーカイブ展開後の全ファイル数 [読み取り専用]
        /// </summary>
        public static readonly uint yum3_progress_max = 0x63DC + 1;

        /// <summary>
        /// フラグ変数
        /// </summary>
        public static bool perlflag = false, workerflag = false, doworkerflag = false, decompress_ok = false, recomp_check_1 = false, recomp_check_2 = false, recompress_ok = false;

        /// <summary>
        /// YUMFILE_*のフラグ変数
        /// 
        /// -1:なし
        /// 0:パーフェクトサン
        /// 1:ワンダリングスター
        /// 2:ミッシングムーン
        /// </summary>
        public static int yumflag = -1;

        /// <summary>
        /// 読み込み先パスを格納しておく一時的な静的変数
        /// </summary>
        public static string current_filepath;

        /// <summary>
        /// 保存先パスを格納しておく一時的な静的変数
        /// </summary>
        public static string current_savepath;

        /// <summary>
        /// YUMFILE_*の種類を格納しておく一時的な静的変数
        /// </summary>
        public static string current_yum;
    }

    /// <summary>
    /// 関数用クラス
    /// </summary>
    class Utils
    {
        /// <summary>
        /// Process.Start: Open URI for .NET Core
        /// </summary>
        /// <param name="URI">http://~ または https://~ から始まるウェブサイトのURL</param>
        public static void OpenURI(string URI)
        {
            try
            {
                Process.Start(URI);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    //Windowsのとき  
                    URI = URI.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {URI}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    //Linuxのとき  
                    Process.Start("xdg-open", URI);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    //Macのとき  
                    Process.Start("open", URI);
                }
                else
                {
                    throw;
                }
            }

            return;
        }

        public static long GetFileSize(string filePath)
        {
            FileInfo fileInfo = new(filePath);
            fileInfo.Refresh();
            if (File.Exists(filePath))
            {
                return fileInfo.Length;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 指定したディレクトリのひとつ前のディレクトリパスを取得
        /// </summary>
        /// <param name="path">ディレクトリのパス</param>
        /// <returns></returns>
        public static string GetLastDirectory(string path)
        {
            var path1 = path;
            var path2 = path1.Substring(0, path1.LastIndexOf(@"\") + 1);
            return path2;
        }

        /// <summary>
        /// 指定したディレクトリのサイズを取得
        /// </summary>
        /// <param name="directoryInfo">サイズを取得するディレクトリのパス</param>
        /// <returns></returns>
        public static long GetDirectorySize(DirectoryInfo directoryInfo)
        {
            long size = 0;
            foreach(FileInfo fi in directoryInfo.GetFiles())
            {
                size += fi.Length;
            }

            foreach(DirectoryInfo di in directoryInfo.GetDirectories())
            {
                size += GetDirectorySize(di);
            }

            return size;
        }

        /// <summary>
        /// 指定したディレクトリごと全て削除
        /// </summary>
        /// <param name="targetDirectoryPath">削除するディレクトリのパス</param>
        public static void DeleteAll(string targetDirectoryPath)
        {
            if (!Directory.Exists(targetDirectoryPath))
            {
                return;
            }

            string[] filePaths = Directory.GetFiles(targetDirectoryPath);
            foreach (string filePath in filePaths)
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
                File.Delete(filePath);
            }

            string[] directoryPaths = Directory.GetDirectories(targetDirectoryPath);
            foreach (string directoryPath in directoryPaths)
            {
                DeleteAll(directoryPath);
            }

            Directory.Delete(targetDirectoryPath, false);
        }

        public static void TemporaryFileCheckExists()
        {
            if (Directory.Exists(Directory.GetCurrentDirectory() + "\\tmp"))
            {
                DeleteAll(Directory.GetCurrentDirectory() + "\\tmp");
            }
            if (File.Exists(Directory.GetCurrentDirectory() + "\\lzss.exe"))
            {
                File.Delete(Directory.GetCurrentDirectory() + "\\lzss.exe");
            }
            return;
        }

        /// <summary>
        /// Indexファイルの整合性チェック
        /// </summary>
        /// <param name="indexFilePath">Indexファイルのパス</param>
        /// <returns>
        /// 真偽値
        /// true: OK
        /// false: NG
        /// </returns>
        public static bool CheckIndexFile(string indexFilePath)
        {
            try
            {
                FileStream fileStream = new(indexFilePath, FileMode.Open);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                StreamReader streamReader = new(fileStream, Encoding.GetEncoding("Shift_JIS"));
                while (streamReader.EndOfStream == false)
                {
                    string line = streamReader.ReadLine();
                    if (line.Contains("\\"))
                    {
                        if (line.Contains("C\t"))
                        {
                            if (Directory.Exists(line.Replace("C\t", "").Substring(0, line.Replace("C\t", "").LastIndexOf(@"/") + 1)))
                            {
                                continue;
                            }
                            else
                            {
                                fileStream.Close();
                                streamReader.Close();
                                MessageBox.Show(null, string.Format(Localize.UnexpectedIndexNotFoundPath, line.Replace("C\t", "").Substring(0, line.Replace("C\t", "").LastIndexOf(@"/") + 1)), Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                        else
                        {
                            if (Directory.Exists(line.Replace("\t", "").Substring(0, line.Replace("\t", "").LastIndexOf(@"/") + 1)))
                            {
                                continue;
                            }
                            else
                            {
                                fileStream.Close();
                                streamReader.Close();
                                MessageBox.Show(null, string.Format(Localize.UnexpectedIndexNotFoundPath, line.Replace("\t", "").Substring(0, line.Replace("\t", "").LastIndexOf(@"/") + 1)), Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                fileStream.Close();
                streamReader.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, Localize.UnexpectedError + "\r\n\r\n" + ex.ToString(), Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static string MD5Hash(string file)
        {
            using var fs = new System.IO.FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            var md5 = System.Security.Cryptography.MD5.Create();
            var md5Hash = md5.ComputeHash(fs);
            return BitConverter.ToString(md5Hash);
        }
    }
}


namespace PrivateProfile
{
    public class IniFile
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern uint GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        /// <summary>
        /// Ini ファイルのファイルパスを取得、設定します。
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="filePath">Ini ファイルのファイルパス</param>
        public IniFile(string filePath)
        {
            FilePath = filePath;
        }
        /// <summary>
        /// Ini ファイルから文字列を取得します。
        /// </summary>
        /// <param name="section">セクション名</param>
        /// <param name="key">項目名</param>
        /// <param name="defaultValue">値が取得できない場合の初期値</param>
        /// <returns></returns>
        public string GetString(string section, string key, string defaultValue = "")
        {
            var sb = new StringBuilder(1024);
            _ = GetPrivateProfileString(section, key, defaultValue, sb, (uint)sb.Capacity, FilePath);
            return sb.ToString();
        }
        /// <summary>
        /// Ini ファイルから整数を取得します。
        /// </summary>
        /// <param name="section">セクション名</param>
        /// <param name="key">項目名</param>
        /// <param name="defaultValue">値が取得できない場合の初期値</param>
        /// <returns></returns>
        public int GetInt(string section, string key, int defaultValue = 0)
        {
            return (int)GetPrivateProfileInt(section, key, defaultValue, FilePath);
        }
        /// <summary>
        /// Ini ファイルに文字列を書き込みます。
        /// </summary>
        /// <param name="section">セクション名</param>
        /// <param name="key">項目名</param>
        /// <param name="value">書き込む値</param>
        /// <returns></returns>
        public bool WriteString(string section, string key, string value)
        {
            return WritePrivateProfileString(section, key, value, FilePath);
        }
    }
}
