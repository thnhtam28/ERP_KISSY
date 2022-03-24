namespace ERP_API.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using System.Linq;
    using WebMatrix.WebData;
    using System.Web;
    using System.Configuration;
    using System.Net.Mail;
    using System.Net.Mime;
    using System.Net;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.IO;
    using System.Data;
    using System.ComponentModel;
    using System.Globalization;
    using Erp.Domain;
    using Erp.Domain.Repositories;

    public class Common
    {
        #region function datetime
    
        public static string FormatDateTime(object value)
        {
            return Convert.ToDateTime(value).ToString("HH:mm - dd/MM/yyyy");
        }

        public static double CalculateTwoDates(DateTime start, DateTime end, string valueGet = "days")
        {
            TimeSpan subtractValue = end.Subtract(start);
            //DateTime diff1 = e.Subtract(subtractValue);
            double returnValue = 0;

            switch (valueGet)
            {
                case "days":
                    returnValue = subtractValue.Days;
                    break;
                case "totaldays":
                    returnValue = subtractValue.TotalDays;
                    break;
                case "houses":
                    returnValue = subtractValue.Hours;
                    break;
                case "totalhouses":
                    returnValue = subtractValue.TotalHours;
                    break;
                case "minutes":
                    returnValue = subtractValue.Minutes;
                    break;
                case "totalminutes":
                    returnValue = subtractValue.TotalMinutes;
                    break;
                case "seconds":
                    returnValue = subtractValue.Seconds;
                    break;
                case "totalseconds":
                    returnValue = subtractValue.TotalSeconds;
                    break;
                case "ticks":
                    returnValue = subtractValue.Ticks;
                    break;
                default:
                    returnValue = subtractValue.Days;
                    break;
            }

            return returnValue;

        }
        #endregion

        public static string[] fieldsNoReplace = new string[] { "Id", "IsDeleted", "CreatedDate", "CreatedUserId", "ModifiedDate", "ModifiedUserId", "AssignedUserId", "StaffId",
                "Month", "Year", "TimekeepingListId", "AssignedUserId", "CommercialCreditId", "DayEffective", "DayDecision", "LevelPay"};

    
        public static void WriteEventLog(String logData)
        {
            if (!EventLog.SourceExists("ErpPlus"))
            {
                //An event log source should not be created and immediately used.
                //There is a latency time to enable the source, it should be created
                //prior to executing the application that uses the source.
                //Execute this sample a second time to use the new source.
                EventLog.CreateEventSource("ErpPlus", "ErpPlusLog");
                Console.WriteLine("CreatedEventSource");
                Console.WriteLine("Exiting, execute the application a second time to use the source.");
                // The source is created.  Exit the application to allow it to be registered.
                return;
            }

            // Create an EventLog instance and assign its source.
            EventLog myLog = new EventLog();
            myLog.Source = "ErpPlus";

            // Write an informational entry to the event log.    
            myLog.WriteEntry(logData);
        }
        public static string StripHTML(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public static decimal NVL_NUM_DECIMAL_NEW(object str)
        {
            if ((str != System.DBNull.Value) && (str != null))
            {
                if (str.ToString().Trim().Equals("") == false)
                {
                    return decimal.Parse(str.ToString());
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }


        public static string NVL_NUM_STRING_NEW(object str)
        {
            if ((str != System.DBNull.Value) && (str != null))
            {
                if (str.ToString().Trim().Equals("") == false)
                {
                    return str.ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }


        public static object NVL_NUM_DECIMAL_object(object str)
        {
            if ((str != System.DBNull.Value) && (str != null))
            {
                if (str.ToString().Trim().Equals("") == false)
                {
                    return double.Parse(str.ToString()).ToString();
                }
                else
                {
                    return System.DBNull.Value;
                }
            }
            else
            {
                return System.DBNull.Value;
            }
        }


        public static int NVL_NUM_NT_NEW(object str)
        {
            if ((str != System.DBNull.Value) && (str != null))
            {
                if (str.Equals("") == false)
                {
                    return int.Parse(str.ToString());
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        public static long NVL_NUM_LONG_NEW(object str)
        {
            if ((str != System.DBNull.Value) && (str != null))
            {
                if (str.ToString().Trim().Equals("") == false)
                {
                    return long.Parse(str.ToString());
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public static string PhanCachHangNgan(object str)
        {
            try
            {
                if (Convert.ToInt32(str) >= 1000)
                    return Convert.ToInt32(str).ToString("0,000").Replace(",", ".");
                else
                    return str.ToString();
            }
            catch { }
            return "";
        }
        public static string PhanCachHangNgan2(object str)
        {
            try
            {
                if (Convert.ToDecimal(str) >= 1000)
                    return Convert.ToDecimal(str).ToString("0,000").Replace(",", ".");
                else
                    return str.ToString();
            }
            catch { }
            return "";
        }

     
        public static string Capitalize(string value)
        {
            if (string.IsNullOrEmpty(value))
                value = "";
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.Trim().ToLower());
        }

        public static string ChuyenThanhKhongDau(string s)
        {
            if (string.IsNullOrEmpty(s) == true)
                return "";

            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToLower();
        }

        public static string ChuyenSoThanhChu(string number)
        {
            string[] strTachPhanSauDauPhay;
            if (number.Contains(".") || number.Contains(","))
            {
                strTachPhanSauDauPhay = number.Split(',', '.');
                return (ChuyenSoThanhChu(strTachPhanSauDauPhay[0]) + "phẩy " + ChuyenSoThanhChu(strTachPhanSauDauPhay[1]));
            }

            string[] dv = { "", "mươi", "trăm", "nghìn", "triệu", "tỉ" };
            string[] cs = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string doc;
            int i, j, k, n, len, found, ddv, rd;

            len = number.Length;
            number += "ss";
            doc = "";
            found = 0;
            ddv = 0;
            rd = 0;

            i = 0;
            while (i < len)
            {
                //So chu so o hang dang duyet
                n = (len - i + 2) % 3 + 1;

                //Kiem tra so 0
                found = 0;
                for (j = 0; j < n; j++)
                {
                    if (number[i + j] != '0')
                    {
                        found = 1;
                        break;
                    }
                }

                //Duyet n chu so
                if (found == 1)
                {
                    rd = 1;
                    for (j = 0; j < n; j++)
                    {
                        ddv = 1;
                        switch (number[i + j])
                        {
                            case '0':
                                if (n - j == 3) doc += cs[0] + " ";
                                if (n - j == 2)
                                {
                                    if (number[i + j + 1] != '0') doc += "linh ";
                                    ddv = 0;
                                }
                                break;
                            case '1':
                                if (n - j == 3) doc += cs[1] + " ";
                                if (n - j == 2)
                                {
                                    doc += "mười ";
                                    ddv = 0;
                                }
                                if (n - j == 1)
                                {
                                    if (i + j == 0) k = 0;
                                    else k = i + j - 1;

                                    if (number[k] != '1' && number[k] != '0')
                                        doc += "mốt ";
                                    else
                                        doc += cs[1] + " ";
                                }
                                break;
                            case '5':
                                if ((i + j == len - 1) || (i + j + 3 == len - 1))
                                    doc += "năm ";
                                else
                                    doc += cs[5] + " ";
                                break;
                            default:
                                doc += cs[(int)number[i + j] - 48] + " ";
                                break;
                        }

                        //Doc don vi nho
                        if (ddv == 1)
                        {
                            doc += ((n - j) != 1) ? dv[n - j - 1] + " " : dv[n - j - 1];
                        }
                    }
                }


                //Doc don vi lon
                if (len - i - n > 0)
                {
                    if ((len - i - n) % 9 == 0)
                    {
                        if (rd == 1)
                            for (k = 0; k < (len - i - n) / 9; k++)
                                doc += "tỉ ";
                        rd = 0;
                    }
                    else
                        if (found != 0) doc += dv[((len - i - n + 1) % 9) / 3 + 2] + " ";
                }

                i += n;
            }

            if (len == 1)
                if (number[0] == '0' || number[0] == '5') return cs[(int)number[0] - 48];

            return FirstCharToUpper(doc);
        }

        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static string ChuyenSoThanhChu(int number)
        {
            return ChuyenSoThanhChu(number.ToString());
        }

        public static string GetUserSetting(string key)
        {
            try
            {
                UserSettingRepository userSettingRepository = new UserSettingRepository(new ErpDbContext());

                return userSettingRepository.GetUserSettingByKey(key, WebMatrix.WebData.WebSecurity.CurrentUserId);
            }
            catch { }

            return null;
        }

        public static string GetCode(string prefix, int value, int lenght = 6)
        {
            var numberStr = value.ToString();
            while (numberStr.Length < lenght)
            {
                numberStr = "0" + numberStr;
            }

            //return prefix + Erp.BackOffice.Helpers.Common.CurrentUser.BranchCode + numberStr;
            return prefix + numberStr;
            //var value = GetUserSetting(key);
            //[!@#$%^&*(){}|\?><:"",./~`=+_-] //separators
            //var result = Regex.Split(value, @"[^a-zA-Z0-9]"); //text:HD_1  => [0]:HD, [1]:1
            //var result2 = Regex.Split(value, @"^[a-zA-Z0-9]"); //text:HD_1  => [0]:"", [1]:D_1

            //if (string.IsNullOrEmpty(value))
            //    return "";

            //var onlyNumber = Regex.Replace(value, @"[^0-9]", "");

            //int number;
            //if (int.TryParse(onlyNumber, out number))
            //{
            //    number += valueChange;
            //    string numberStr = number.ToString();
            //    while (numberStr.Length < onlyNumber.Length)
            //    {
            //        numberStr = "0" + numberStr;
            //    }

            //    return value.Replace(onlyNumber, numberStr);
            //}


            //return value;
        }

        //begin hoa moi  them vao

       




        public static string GetCode_mobile(string prefix, int value, string BranchCode, int lenght = 6)
        {
            var numberStr = value.ToString();
            while (numberStr.Length < lenght)
            {
                numberStr = "0" + numberStr;
            }

            return prefix + BranchCode + numberStr;

            //var value = GetUserSetting(key);
            //[!@#$%^&*(){}|\?><:"",./~`=+_-] //separators
            //var result = Regex.Split(value, @"[^a-zA-Z0-9]"); //text:HD_1  => [0]:HD, [1]:1
            //var result2 = Regex.Split(value, @"^[a-zA-Z0-9]"); //text:HD_1  => [0]:"", [1]:D_1

            //if (string.IsNullOrEmpty(value))
            //    return "";

            //var onlyNumber = Regex.Replace(value, @"[^0-9]", "");

            //int number;
            //if (int.TryParse(onlyNumber, out number))
            //{
            //    number += valueChange;
            //    string numberStr = number.ToString();
            //    while (numberStr.Length < onlyNumber.Length)
            //    {
            //        numberStr = "0" + numberStr;
            //    }

            //    return value.Replace(onlyNumber, numberStr);
            //}


            //return value;
        }

     

        public static string GetPathImageMobile()
        {
            try
            {

                return ConfigurationManager.AppSettings.Get("pathImageMobile");
            }
            catch { }

            return null;
        }



        public static string GetSetting(string key)
        {
            try
            {
                SettingRepository settingRepository = new SettingRepository(new ErpDbContext());

                return settingRepository.GetSettingByKey(key).Value;
            }
            catch { }

            return null;
        }


        public static string GetSetting_Script(string key)
        {
            try
            {
                SettingRepository settingRepository = new SettingRepository(new ErpDbContext());

                return settingRepository.GetSettingByKey(key).Note;
            }
            catch { }

            return null;
        }


        public static string MoneyToString(object str)
        {
            try
            {
                if (Convert.ToInt64(str) >= 1000)
                    return Convert.ToInt64(str).ToString("0,000").Replace(".", ",");
                else
                    return str.ToString();
            }
            catch { }
            return "";
        }
     

        public static string GetWebConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }



        public static bool SendEmail(string toEmail, string subj, string body)
        {
            string emailFrom = "";
            string emailPasswordFrom = "";
            string port = "";
            string smtp = "";

            if (ConfigurationManager.AppSettings["EmailSend"] != null)
                emailFrom = ConfigurationManager.AppSettings["EmailSend"].ToString();
            if (ConfigurationManager.AppSettings["EmailPassWord"] != null)
                emailPasswordFrom = ConfigurationManager.AppSettings["EmailPassWord"].ToString();
            if (ConfigurationManager.AppSettings["EmailHost"] != null)
                smtp = ConfigurationManager.AppSettings["EmailHost"].ToString();
            if (ConfigurationManager.AppSettings["EmailPort"] != null)
                port = ConfigurationManager.AppSettings["EmailPort"].ToString();

            string subject = "Thông tin Feedback: " + toEmail + " - " + subj;





            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Host = smtp;
                    smtpClient.Port = Convert.ToInt32(port);
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential(emailFrom, emailPasswordFrom);
                    var msg = new MailMessage
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8,
                        From = new MailAddress(emailFrom),
                        Subject = subject,
                        Body = body,
                        Priority = MailPriority.Normal,
                    };
                    msg.To.Add(emailFrom);
                    smtpClient.Send(msg);
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public static bool SendEmailAttachment(string emailFrom, string emailPasswordFrom, string SentTo, string subject, string body, string cc, string bcc, string displayName, string filePath = null, string fileNameDisplayHasExtention = null)
        {

            //string from = System.Configuration.ConfigurationManager.AppSettings["Email"];
            //string password = System.Configuration.ConfigurationManager.AppSettings["Email_Password"];
            string port = System.Configuration.ConfigurationManager.AppSettings["Port"];
            string ssl = System.Configuration.ConfigurationManager.AppSettings["SSL"];
            string smtp = System.Configuration.ConfigurationManager.AppSettings["SMTP"];

            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(emailFrom, displayName);
            msg.To.Add(SentTo);

            if (string.IsNullOrEmpty(cc) == false)
                msg.CC.Add(cc);

            if (string.IsNullOrEmpty(bcc) == false)
                msg.Bcc.Add(bcc);

            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;

            if (string.IsNullOrEmpty(filePath) == false)
            {
                System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(filePath, new ContentType(MediaTypeNames.Application.Octet));
                //attachment.TransferEncoding = System.Net.Mime.TransferEncoding.;
                attachment.ContentDisposition.FileName = fileNameDisplayHasExtention;
                //attachment.ContentDisposition.Size = attachment.ContentStream.Length;
                msg.Attachments.Add(attachment);
            }

            SmtpClient client = new SmtpClient();

            client.Host = smtp;
            client.Port = Convert.ToInt32(port);
            client.UseDefaultCredentials = false;

            if (ssl.ToLower() == "true")
                client.EnableSsl = true;
            else
                client.EnableSsl = false;

            client.Credentials = new NetworkCredential(emailFrom, emailPasswordFrom);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;


            try
            {
                client.Send(msg);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        public static string ChuyenSoThanhChu_2(string number)
        {
            string[] strTachPhanSauDauPhay;
            if (number.Contains(".") || number.Contains(","))
            {
                strTachPhanSauDauPhay = number.Split(',', '.');
                return (ChuyenSoThanhChu_2(strTachPhanSauDauPhay[0]) + " phẩy " + ChuyenSoThanhChu_2(strTachPhanSauDauPhay[1]));
            }

            string[] dv = { "", "mươi", "trăm", "nghìn", "triệu", "tỉ" };
            string[] cs = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string doc;
            int i, j, k, n, len, found, ddv, rd;

            len = number.Length;
            number += "ss";
            doc = "";
            found = 0;
            ddv = 0;
            rd = 0;

            i = 0;
            while (i < len)
            {
                //So chu so o hang dang duyet
                n = (len - i + 2) % 3 + 1;

                //Kiem tra so 0
                found = 0;
                for (j = 0; j < n; j++)
                {
                    if (number[i + j] != '0')
                    {
                        found = 1;
                        break;
                    }
                }

                //Duyet n chu so
                if (found == 1)
                {
                    rd = 1;
                    for (j = 0; j < n; j++)
                    {
                        ddv = 1;
                        switch (number[i + j])
                        {
                            case '0':
                                if (n - j == 3) doc += cs[0] + " ";
                                if (n - j == 2)
                                {
                                    if (number[i + j + 1] != '0') doc += "lẻ ";
                                    ddv = 0;
                                }
                                break;
                            case '1':
                                if (n - j == 3) doc += cs[1] + " ";
                                if (n - j == 2)
                                {
                                    doc += "mười ";
                                    ddv = 0;
                                }
                                if (n - j == 1)
                                {
                                    if (i + j == 0) k = 0;
                                    else k = i + j - 1;

                                    if (number[k] != '1' && number[k] != '0')
                                        doc += "mốt ";
                                    else
                                        doc += cs[1] + " ";
                                }
                                break;
                            case '5':
                                if ((i + j == len - 1) || (i + j + 3 == len - 1))
                                    doc += "năm ";
                                else
                                    doc += cs[5] + " ";
                                break;
                            default:
                                doc += cs[(int)number[i + j] - 48] + " ";
                                break;
                        }

                        //Doc don vi nho
                        if (ddv == 1)
                        {
                            doc += ((n - j) != 1) ? dv[n - j - 1] + " " : dv[n - j - 1];
                        }
                    }
                }


                //Doc don vi lon
                if (len - i - n > 0)
                {
                    if ((len - i - n) % 9 == 0)
                    {
                        if (rd == 1)
                            for (k = 0; k < (len - i - n) / 9; k++)
                                doc += "tỉ ";
                        rd = 0;
                    }
                    else
                        if (found != 0) doc += dv[((len - i - n + 1) % 9) / 3 + 2] + " ";
                }

                i += n;
            }

            if (len == 1)
                if (number[0] == '0' || number[0] == '5') return cs[(int)number[0] - 48];

            if (!string.IsNullOrEmpty(number))
            {
                var ky_tu_cuoi = number[len - 1].ToString();
                if (ky_tu_cuoi == "0")
                {
                    doc = doc + " đồng chẵn";
                }
                else
                {
                    doc = doc + " đồng";
                }
            }

            return doc;
        }

        public static string ChuyenSoThanhChu_2(int number)
        {
            return ChuyenSoThanhChu_2(number.ToString());
        }
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }
        //hàm convert từ byte sang kilobyte, MB, GB
        public static string GetFileSize(double byteCount)
        {
            string size = "0 Bytes";
            if (byteCount >= 1073741824.0)
                size = String.Format("{0:##.##}", byteCount / 1073741824.0) + " GB";
            else if (byteCount >= 1048576.0)
                size = String.Format("{0:##.##}", byteCount / 1048576.0) + " MB";
            else if (byteCount >= 1024.0)
                size = String.Format("{0:##.##}", byteCount / 1024.0) + " KB";
            else if (byteCount > 0 && byteCount < 1024.0)
                size = byteCount.ToString() + " Bytes";

            return size;
        }

        public static string GetCodebyBranch(string prefix, int value, string BranchCode, int lenght = 6)
        {
            var numberStr = value.ToString();
            while (numberStr.Length < lenght)
            {
                numberStr = "0" + numberStr;
            }

            return prefix + BranchCode + numberStr;
        }

        public static bool IsNumber(string text)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(text);
        }

        public static string PhanCachHangNgan_string_number(object str)
        {
            if (IsNumber(str.ToString().Replace(",", ".")))
            {
                try
                {
                    if (Convert.ToDecimal(str) >= 1000)
                        return PhanCachHangNgan(Convert.ToDecimal(str)).Replace(",", ".");
                    else
                    {
                        if (Convert.ToDecimal(str) == 0)
                            return "-";
                        else
                            return str.ToString();
                    }
                }
                catch
                {
                    return "-";
                }
            }
            return str.ToString();

        }

        public static string CanhLeTungDongTable(object str)
        {
            var style = "";
            if (IsNumber(str.ToString().Replace(",", ".")))
            {
                style = "text-right";
            }
            else
            {
                style = "text-left";
            }
            return style;
        }

        public static bool KiemTraNgaySuaChungTu(DateTime CreatedDate)
        {
            var limit_daterange_for_update_data = GetSetting("limit_daterange_for_update_data");
            if (string.IsNullOrEmpty(limit_daterange_for_update_data))
            {
                limit_daterange_for_update_data = "0";
            }

            if (CreatedDate.AddDays(Convert.ToInt32(limit_daterange_for_update_data)) > DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string GetUrlImage(string url, string folder, string type)
        {
            string host = "";
            if (ConfigurationManager.AppSettings["UrlAPI"] != null)
                host = ConfigurationManager.AppSettings["UrlAPI"].ToString();
            return host + KiemTraTonTaiHinhAnh(url, folder, type);
        }
        public static string KiemTraTonTaiHinhAnh(string Image, string NameUrlImage, string NoImage)
        {
            //NameUrlImage là cột code trong setting .
            var ImageUrl = "";
            //chọn thay thế ảnh khi không có tên hình trong database.
            switch (NoImage)
            {
                case "product":
                    NoImage = "/assets/css/images/noimage.gif";
                    break;
                case "productgroups":
                    NoImage = "/assets/css/images/noimage.gif";
                    break;

                case "producttypes":
                    NoImage = "/assets/css/images/noimage.gif";
                    break;

                case "dealhot":
                    NoImage = "/assets/css/images/noimage.gif";
                    break;

                case "tinkhuyenmai":
                    NoImage = "/assets/css/images/noimage.gif";
                    break;

                case "tintuc":
                    NoImage = "/assets/css/images/noimage.gif";
                    break;

                case "tintuckhac":
                    NoImage = "/assets/css/images/noimage.gif";
                    break;
                case "baochi":
                    NoImage = "/assets/css/images/noimage.gif";
                    break;
                case "nghesy":
                    NoImage = "/assets/css/images/noimage.gif";
                    break;
                case "nhomtin":
                    NoImage = "/assets/css/images/noimage.gif";
                    break;


                case "banner":
                    NoImage = "/assets/css/images/noimage.gif";
                    break;
                case "user":
                    NoImage = "/assets/img/no-avatar.png";
                    break;
                case "service":
                    NoImage = "/assets/css/images/noimage.gif";
                    break;
                case "maytinhbang":
                    NoImage = "/assets/css/images/maytinhbang.jpg";
                    break;
            }
            //lấy đường dẫn hình ảnh
            var ImagePath = GetSetting(NameUrlImage);
            var filepath = System.Web.HttpContext.Current.Server.MapPath("~" + ImagePath);
            //nếu có hình ảnh
            if (!string.IsNullOrEmpty(Image))
            {
                ImageUrl = ImagePath + Image;
                string url = filepath + Image;
                //kiểm tra hình ảnh có tồn tại hay không..
                if (!System.IO.File.Exists(filepath + Image))
                {
                    ImageUrl = NoImage;
                }
                else
                {
                    ImageUrl = ImagePath + Image;
                }
            }
            else
                //không có ảnh
                if (string.IsNullOrEmpty(Image))
            {
                ImageUrl = NoImage;
            }
            return ImageUrl;
        }
        public static string KiemTraGioiTinh(bool? GioiTinh)
        {
            //NameUrlImage là cột code trong setting .
            var IconGioiTinh = "";
            //chọn thay thế ảnh khi không có tên hình trong database.
            switch (GioiTinh)
            {
                case null:
                    IconGioiTinh = "";
                    break;
                case true:
                    IconGioiTinh = "<i style=\"color:#ff00dc\" class=\"fa fa-female\" data-rel=\"tooltip\" title=\"\" data-placement=\"bottom\" data-original-title=\"Giới tính: Nữ\"></i>";
                    break;
                case false:
                    IconGioiTinh = "<i class=\"fa fa-male\" data-rel=\"tooltip\" title=\"\" data-placement=\"bottom\" data-original-title=\"Giới tính: Nam\"></i>";
                    break;
            }

            return IconGioiTinh;
        }

        public static DateTime GetYesterday()
        {
            // Ngày hôm nay.
            DateTime today = DateTime.Today;

            // Trừ đi một ngày.
            return today.AddDays(-1);
        }

        // Trả về ngày đầu tiên của năm
        public static DateTime GetFirstDayInYear(int year)
        {
            DateTime aDateTime = new DateTime(year, 1, 1);
            return aDateTime;
        }

        // Trả về ngày cuối cùng của năm.
        public static DateTime GetLastDayInYear(int year)
        {
            DateTime aDateTime = new DateTime(year + 1, 1, 1);

            // Trừ đi một ngày.
            DateTime retDateTime = aDateTime.AddDays(-1);

            return retDateTime;
        }

        // Trả về ngày đầu tiên của tháng
        public static DateTime GetFistDayInMonth(int year, int month)
        {
            DateTime aDateTime = new DateTime(year, month, 1);

            return aDateTime;
        }

        // Trả về ngày cuối cùng của tháng.
        public static DateTime GetLastDayInMonth(int year, int month)
        {
            DateTime aDateTime = new DateTime(year, month, 1);

            // Cộng thêm 1 tháng và trừ đi một ngày.
            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);

            return retDateTime;
        }

        public static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3);
        }


    }

    public static class CommonSatic
    {
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static string ToDecimal4String(this decimal? value)
        {
            if (value.GetValueOrDefault(0) == 0) return string.Empty;
            return value.GetValueOrDefault(0).ToString("##.##0,00");
        }
        public static string ToCurrencyStr(this decimal? value, string currency)
        {
            if (value.GetValueOrDefault(0) == 0) return "0";
            if (string.IsNullOrEmpty(currency))
                return value.GetValueOrDefault(0).ToString("##,###");
            if (currency.ToUpper() == "VND")
                return value.GetValueOrDefault(0).ToString("##,###");
            else
                return value.GetValueOrDefault(0).ToString("##,##0.00");
        }
    }

    public class DateDifference
    {

        public static string DateDifferences(DateTime d1, DateTime d2)
        {
            int[] monthDay = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int increment;
            DateTime fromDate;
            DateTime toDate;
            int month;
            int year;
            int day;
            if (d1 > d2)
            {
                fromDate = d2;
                toDate = d1;
            }
            else
            {
                fromDate = d1;
                toDate = d2;
            }

            /// 
            /// Day Calculation
            /// 
            increment = 0;

            if (fromDate.Day > toDate.Day)
            {
                increment = monthDay[fromDate.Month - 1];

            }
            /// if it is february month
            /// if it's to day is less then from day
            if (increment == -1)
            {
                if (DateTime.IsLeapYear(fromDate.Year))
                {
                    // leap year february contain 29 days
                    increment = 29;
                }
                else
                {
                    increment = 28;
                }
            }
            if (increment != 0)
            {
                day = (toDate.Day + increment) - fromDate.Day;
                increment = 1;
            }
            else
            {
                day = toDate.Day - fromDate.Day;
            }

            ///
            /// month calculation
            ///
            if ((fromDate.Month + increment) > toDate.Month)
            {
                month = (toDate.Month + 12) - (fromDate.Month + increment);
                increment = 1;
            }
            else
            {
                month = (toDate.Month) - (fromDate.Month + increment);
                increment = 0;
            }

            ///
            /// year calculation
            ///
            year = toDate.Year - (fromDate.Year + increment);
            string retur = year + " year, " + month + " month, " + day + " day";
            return retur.ToString();
        }

    }


}


