using FEUtility.Mail.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FEUtility.Mail
{
    public class SendMail
    {
        Stopwatch watch = new Stopwatch();
        public List<string> filePaths = new List<string>();
        List<string> FilePaths
        {
            get
            {
                if (filePaths == null)
                    filePaths = new List<string>();
                return filePaths;
            }
        }
        public string m_to { get; set; }
        public string m_cc { get; set; }
        public string m_bcc { get; set; }
        public string m_Subject { get; set; }
        public string m_Body { get; set; }
        public int MainCount { get; set; }

        public List<string> listToUser { get; set; }
        public SendMail()
        {
            m_to = "";
            m_cc = "";
            m_bcc = "";
            m_Subject = "";
            m_Body = "";
        }

        public void SendMsg()
        {
            watch.Reset();
            watch.Start();
            long count = MainCount;
            MailHelper mail = new MailHelper(true);
            for (long i = 1; i <= count; i++)
            {
                m_to = listToUser[Convert.ToInt32(i) - 1];
                this.SendMessageAsync(mail, false, "实验二", "第" + i + "条", true, true);
            }
            mail.SetBatchMailCount(count);
        }

        /// <summary>
        /// 异步发送邮件
        /// </summary>
        /// <param name="isSimple">是否只发送一条</param>
        /// <param name="autoReleaseSmtp">是否自动释放SmtpClient</param>
        /// <param name="isReuse">是否重用SmtpClient</param>
        private void SendMessageAsync(MailHelper mail, bool isSimple, string shiyan, string msgCount, bool autoReleaseSmtp, bool isReuse)
        {
            LogHelper.Info(String.Format("{0}：{1}\"异步\"邮件开始。{2}{3}", shiyan, msgCount, watch.ElapsedMilliseconds, Environment.NewLine));

            if (!isReuse || !mail.ExistsSmtpClient())
            {
                SmtpClient client = new SmtpHelper(EmailConfig.TestEmailType, false, EmailConfig.TestUserName, EmailConfig.TestPassword).SmtpClient;
                mail.AsycUserState = String.Format("{0}：{1}\"异步\"邮件", shiyan, msgCount);
                client.SendCompleted += (send, args) =>
                {
                    AsyncCompletedEventArgs arg = args;

                    if (arg.Error == null)
                    {
                        // 需要注意的事使用 MailHelper 发送异步邮件，其UserState是 MailUserState 类型
                        LogHelper.Info(((MailUserState)args.UserState).UserState.ToString() + "已发送完成." + watch.ElapsedMilliseconds + Environment.NewLine);
                    }
                    else
                    {
                        LogHelper.Info(String.Format("{0} 异常：{1}{2}{3}"
                            , ((MailUserState)args.UserState).UserState.ToString() + "发送失败."
                            , (arg.Error.InnerException == null ? arg.Error.Message : arg.Error.Message + arg.Error.InnerException.Message)
                            , watch.ElapsedMilliseconds, Environment.NewLine));
                        // 标识异常已处理，否则若有异常，会抛出异常
                        ((MailUserState)args.UserState).IsErrorHandle = true;
                    }
                };
                mail.SetSmtpClient(client, autoReleaseSmtp);
            }
            else
            {
                mail.AsycUserState = String.Format("{0}：{1}\"异步\"邮件已发送完成。", shiyan, msgCount);
            }

            mail.From = EmailConfig.TestFromAddress;
            mail.FromDisplayName = EmailConfig.GetAddressName(EmailConfig.TestFromAddress);

            string to = m_to;
            string cc = m_cc;
            string bcc = m_bcc;
            if (to.Length > 0)
                mail.AddReceive(EmailAddrType.To, to, EmailConfig.GetAddressName(to));
            if (cc.Length > 0)
                mail.AddReceive(EmailAddrType.CC, cc, EmailConfig.GetAddressName(cc));
            if (bcc.Length > 0)
                mail.AddReceive(EmailAddrType.Bcc, bcc, EmailConfig.GetAddressName(bcc));

            mail.Subject = m_Subject;
            // Guid.NewGuid() 防止重复内容，被SMTP服务器拒绝接收邮件
            mail.Body = m_Body + "<div style='display:none'>" + Guid.NewGuid() + "</div>";
            mail.IsBodyHtml = true;

            if (filePaths != null && filePaths.Count > 0)
            {
                foreach (string filePath in FilePaths)
                {
                    mail.AddAttachment(filePath);
                }
            }

            Dictionary<MailInfoType, string> dic = mail.CheckSendMail();
            if (dic.Count > 0 && MailInfoHelper.ExistsError(dic))
            {
                // 反馈“错误+提示”信息
                LogHelper.Info(MailInfoHelper.GetMailInfoStr(dic));
            }
            else
            {
                string msg = String.Empty;
                if (dic.Count > 0)
                {
                    // 反馈“提示”信息
                    msg = MailInfoHelper.GetMailInfoStr(dic);
                }

                try
                {
                    // 发送
                    if (isSimple)
                    {
                        mail.SendOneMail();
                    }
                    else
                    {
                        mail.SendBatchMail();
                    }
                }
                catch (Exception ex)
                {
                    // 反馈异常信息
                    LogHelper.Info(String.Format("{0}\"异步\"异常：（{1}）{2}{3}", msgCount, watch.ElapsedMilliseconds, ex.Message, Environment.NewLine));

                }
                finally
                {
                    // 输出到界面
                    if (msg.Length > 0)
                        LogHelper.Info(msg + Environment.NewLine);
                }
            }

            mail.Reset();
        }


    }
}
