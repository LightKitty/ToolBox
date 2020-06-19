using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToolBox;

namespace ToolBoxTest
{
    [TestClass]
    public class ShiXiSengTest
    {
        const string appId = "sJfCqAupSE4z6ASX";
        const string appSecret = "xFjN3kYvr276FZrkPPmOU2vGpNtkt6tO";

        /// <summary>
        /// 获取用户id
        /// </summary>
        [TestMethod]
        public void UserTest()
        {
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            paramDic.Add("account", "join@mshare.cn");
            paramDic.Add("password", "123456");
            string url = $"http://uat-hr-open.mshare.cn/v1/user";
            string result = ShiXiSengHttp(url, paramDic); //{"code": 100, "data": {"user_uuid": "usr_mzs3yyqlw6p3"}, "msg": "success"}
        }

        /// <summary>
        /// 获取职位列表
        /// </summary>
        [TestMethod]
        public void JobsGetTest()
        {
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            paramDic.Add("user_uuid", "usr_mzs3yyqlw6p3");
            paramDic.Add("page", "1");
            paramDic.Add("limit", "10");
            paramDic.Add("job_type", "normal");
            string url = $"http://uat-hr-open.mshare.cn/v1/jobs";
            string result = ShiXiSengHttp(url, paramDic);
            //{"code": 100, "data": {"total": 67, "page": 1, "data": [{"job_name": "萌想测试11292303科技有限责任公司", "uuid": "inn_c752pyt7hs3r", "job_status": "uncheck", "reject_reason": null, "attraction": ["大牛多", "氛围好"]}, {"job_name": "校园渠道运营专员（正式）", "uuid": "inn_13dsethlpyxo", "job_status": "reject", "reject_reason": "", "attraction": ["明星产品、年轻团队、A轮、扁平管理、五险一金"]}, {"job_name": "手机售前客服", "uuid": "inn_4qwo4zcruhv6", "job_status": "reject", "reject_reason": "", "attraction": ["包住宿，转正5K-7K，"]}, {"job_name": "手机品牌视频演示客服", "uuid": "inn_93q4mn3xvelw", "job_status": "reject", "reject_reason": "", "attraction": ["包住宿，转正5K-7K，"]}, {"job_name": "测试（请不要通过）", "uuid": "inn_v8i7aovigs2k", "job_status": "reject", "reject_reason": "", "attraction": ["fulidaiyufkjldgklsfd"]}, {"job_name": "项目助理", "uuid": "inn_zz3spbmnyy7f", "job_status": "uncheck", "reject_reason": null, "attraction": ["年轻团队", "大牛直带", "快速成长"]}, {"job_name": "用户运营实习生", "uuid": "inn_e9i43arle3dl", "job_status": "pass", "reject_reason": null, "attraction": ["好玩有趣", "脑洞清奇", "团队氛围好", "快速成长"]}, {"job_name": "RPO招聘助理", "uuid": "inn_shr3gukuo8r8", "job_status": "pass", "reject_reason": null, "attraction": ["独挡一面", "快速成长", "氛围nice", "各种福利"]}, {"job_name": "测试哦", "uuid": "inn_e8jpnyckactf", "job_status": "reject", "reject_reason": "", "attraction": ["十分位"]}, {"job_name": "UI设计实习生", "uuid": "inn_fiwd0zsvwjkb", "job_status": "pass", "reject_reason": null, "attraction": ["团队氛围棒", "年轻的团队", "参与百万级平台"]}]}, "msg": "success"}
        }

        /// <summary>
        /// 发布职位
        /// </summary>
        [TestMethod]
        public void JobsPostTest()
        {
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            paramDic.Add("user_uuid", "usr_mzs3yyqlw6p3");
            paramDic.Add("job_cate", "互联网");
            paramDic.Add("job_name", "5萌想测试11292303科技有限责任公司");
            paramDic.Add("province", "string");
            paramDic.Add("city", "string");
            paramDic.Add("county", "高新区");
            paramDic.Add("places", "1");
            paramDic.Add("attraction", "[\"大牛多\", \"氛围好\"]");
            paramDic.Add("info", "<p>01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789</p>");
            paramDic.Add("address", "四川省成都市天府软件园G区7栋7楼");
            paramDic.Add("min_salary", "0");
            paramDic.Add("max_salary", "0");
            paramDic.Add("dayperweek", "5");
            paramDic.Add("lan", "chinese");
            paramDic.Add("degree", "本科");
            paramDic.Add("chance", "notsure");
            paramDic.Add("deliver_email", "join@mshare.cn");
            paramDic.Add("effective_time", "2020-03-07 12:00:00");
            paramDic.Add("update_time", "2019-11-29 23:06:20");
            paramDic.Add("job_status", "uncheck");
            paramDic.Add("is_offline", "false");
            string url = $"http://uat-hr-open.mshare.cn/v1/jobs";
            string result = ShiXiSengHttp(url, paramDic, "POST");
        }

        /// <summary>
        /// 获取职位类别
        /// </summary>
        [TestMethod]
        public void GetJobcatesTest()
        {
            string url = $"https://www.shixiseng.com/api/v1/jobcates";
            string result = ShiXiSengHttp(url);
        }

        /// <summary>
        /// 获取职位详情
        /// </summary>
        [TestMethod]
        public void GetJobDetailTest()
        {
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            paramDic.Add("user_uuid", "usr_mzs3yyqlw6p3");
            string jobId = "inn_c752pyt7hs3r";
            string url = $"http://uat-hr-open.mshare.cn/v1/job/" + jobId;
            string result = ShiXiSengHttp(url, paramDic);
        }

        [TestMethod]
        public void EditJobTest()
        {
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            paramDic.Add("user_uuid", "usr_mzs3yyqlw6p3");
            paramDic.Add("job_cate", "互联网");
            paramDic.Add("job_name", "66萌想测试11292303科技有限责任公司");
            paramDic.Add("province", "string");
            paramDic.Add("city", "string");
            paramDic.Add("county", "高新区");
            paramDic.Add("places", "1");
            paramDic.Add("attraction", "[\"大牛多\", \"氛围好\"]");
            paramDic.Add("info", "<p>01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789</p>");
            paramDic.Add("address", "四川省成都市天府软件园G区7栋7楼");
            paramDic.Add("min_salary", "0");
            paramDic.Add("max_salary", "0");
            paramDic.Add("dayperweek", "5");
            paramDic.Add("lan", "chinese");
            paramDic.Add("degree", "本科");
            paramDic.Add("chance", "notsure");
            paramDic.Add("deliver_email", "join@mshare.cn");
            paramDic.Add("effective_time", "2020-03-07 12:00:00");
            paramDic.Add("update_time", "2019-11-29 23:06:20");
            paramDic.Add("job_status", "uncheck");
            paramDic.Add("is_offline", "false");
            string jobId = "inn_nismj0gwjgvc";
            string url = $"http://uat-hr-open.mshare.cn/v1/job/"+ jobId;
            string result = ShiXiSengHttp(url, paramDic, "POST");
        }

        [TestMethod]
        public void OperateJobTest()
        {
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            paramDic.Add("user_uuid", "usr_mzs3yyqlw6p3");
            paramDic.Add("intern_uuid", "inn_7eiualbowhf3");
            paramDic.Add("operate", "online");
            string url = $"http://uat-hr-open.mshare.cn/v1/job_operation";
            string result = ShiXiSengHttp(url, paramDic, "POST");
        }

        /// <summary>
        /// 获取投递简历列表
        /// </summary>
        [TestMethod]
        public void GetResumesTest()
        {
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            paramDic.Add("user_uuid", "usr_mzs3yyqlw6p3");
            paramDic.Add("intern_uuid", "inn_7eiualbowhf3");
            paramDic.Add("deliver_status", "pend");
            paramDic.Add("limit", "1");
            paramDic.Add("page", "1");
            string url = $"http://uat-hr-open.mshare.cn/v1/resumes";
            string result = ShiXiSengHttp(url, paramDic); //{"code": 100, "data": {"total": 4, "page": 1, "data": [{"uuid": "dvr_86rq7hpqxxyy", "deliver_uuid": "dlv_3gakfs4evazq", "deliver_status": "checked", "user_name": "肖尧", "email": "****email****", "tel": "****tel****", "city": "成都", "degree": "大专", "dayperweek": 5, "month_num": 2, "tags": ["沟通能力", "性格活泼", "责任心强", "团队协作", "形象气质佳"], "entry_date": "2019-07-01", "deliver_time": "2019-05-30 14:56", "head_img": "http://sxsimg.xiaoyuanzhao.com/7E/D9/7EED393A19526DB52B4CC4F5E0A5C2D9.jpg", "interview_time": null, "interview_status": null, "is_check": true, "graduate_year": 2021, "school": "四川航天职业技术学院", "major": "机电一体化技术", "resume_data": {"base": [{"headUrl": "https://sxsimg.xiaoyuanzhao.com/7E/D9/7EED393A19526DB52B4CC4F5E0A5C2D9.jpg", "headUuid": "7EED393A19526DB52B4CC4F5E0A5C2D9", "nickname": "肖尧", "sex": 1, "birth": "1999-10", "age": 20, "city": "成都", "phone": "****tel****", "areaCode": "+86", "email": "****email****"}], "description": [{"desc": "我是一名在校大专生，虽然成绩可能不太优秀，但我有一颗不甘于平凡的心，喜欢去体验新奇的东西，尝试未知的事物！", "descLabel": ["沟通能力", "性格活泼", "责任心强", "团队协作", "形象气质佳", "学习能力"]}], "education": [{"school": "四川航天职业技术学院", "degree": "大专", "date": ["2018", "2021"], "gpa": "", "rank": "中等", "major": "机电一体化技术", "course": "机械制图", "honor": "担任班长一职"}], "experience": [{"company": "私人企业", "date": ["2016-01", "2018-08"], "post": "任何职位", "desc": "虽然我没有去大公司实习过，但是我做过房产销售，营业厅卖过手机，蛋糕店当过营业员，冰淇淋厂里面当过一线工人，也在餐厅里当过服务员。可能这些都是最基本的，但我觉得这些都是通过我自己的努力慢慢累积起的社会经验！"}], "academic": [{"project": "班级管理", "date": ["2018-09", "2019-03"], "job": "班长", "desc": "入学以来一直担任班长一职"}], "prac": [{"project": "飞行系制造系体育部", "date": ["2018-11", "2019-03"], "job": "干事", "desc": "给喜欢体育运动的伙伴创造一个良好的平台，不定期开启各种体育比赛，增强同学之间的相互交流"}], "language": [], "skill": [{"skillName": "机电一体化技术", "desc": "实训过钳工，铣工以及车工"}], "interest": [{"interest": "喜欢跑步，游泳（跑过四次马拉松）"}], "production": [], "attachment": [], "deliverType": "shixiseng", "extraInfo": []}}]}, "msg": "success"}
        }

        /// <summary>
        /// 查看简历详情
        /// </summary>
        [TestMethod]
        public void GetResumeTest()
        {
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            paramDic.Add("user_uuid", "usr_mzs3yyqlw6p3");
            paramDic.Add("deliver_uuid", "dlv_3gakfs4evazq");
            //paramDic.Add("lan", "chinese");
            string url = $"http://uat-hr-open.mshare.cn/v1/resume";
            string result = ShiXiSengHttp(url, paramDic); //{"code": 100, "data": {"stype": "online", "resume_url": "http://uat-hr-open.mshare.cn/v1/resume_html?deliver_uuid=dlv_3gakfs4evazq&lan=chinese&user_uuid=usr_mzs3yyqlw6p3", "real_name": "肖尧", "school": "四川航天职业技术学院", "gender": 1, "major": "机电一体化技术", "degree": "大专", "born_city": "成都", "email": "****email****", "tel": "****tel****", "area_code": "0086"}, "msg": "success"}
        }

        /// <summary>
        /// 简历处理操作（待定/面试/录用/不合适）
        /// </summary>
        [TestMethod]
        public void OperateResume()
        {
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            paramDic.Add("user_uuid", "usr_mzs3yyqlw6p3");
            paramDic.Add("deliver_uuid", "dlv_3gakfs4evazq");
            paramDic.Add("operate", "undeter");
            string url = $"http://uat-hr-open.mshare.cn/v1/resume/operation";
            string result = ShiXiSengHttp(url, paramDic, "POST");
        }

        /// <summary>
        /// 发送面试通知
        /// </summary>
        [TestMethod]
        public void InterviewTest()
        {
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            paramDic.Add("user_uuid", "usr_mzs3yyqlw6p3");
            paramDic.Add("deliver_uuid", "dlv_3gakfs4evazq");
            paramDic.Add("contact", "hr小姐姐");
            paramDic.Add("contact_tel", "88881111");
            paramDic.Add("interview_time", "2020-03-07 12:00:00");
            paramDic.Add("interview_place", "上地");
            paramDic.Add("content", "注意保暖");
            string url = $"http://uat-hr-open.mshare.cn/v1/resume_notice/interview";
            string result = ShiXiSengHttp(url, paramDic, "POST");
        }

        /// <summary>
        /// 发送拒信
        /// </summary>
        [TestMethod]
        public void RejectTest()
        {
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            paramDic.Add("user_uuid", "usr_mzs3yyqlw6p3");
            paramDic.Add("deliver_uuid", "dlv_qymhbg4chirf");
            paramDic.Add("reject_reason", "sorry2");
            string url = $"http://uat-hr-open.mshare.cn/v1/resume_notice/reject";
            string result = ShiXiSengHttp(url, paramDic, "POST");
        }

        public string ShiXiSengHttp(string url, Dictionary<string,string> paramDic = null, string method = "GET")
        {
            long timespan = TTime.GetUnixTimestampNow();
            string sign = TMD5.GenerateMD5(appId + appSecret + timespan, Encoding.UTF8).ToUpper(); //签名
            string authorization = TString.ToBase64String(appId + ":" + timespan); //认证
            StringBuilder sb = new StringBuilder();
            sb.Append("?");
            if(paramDic!=null)
            {
                foreach (var kvPair in paramDic)
                {
                    sb.AppendFormat("{0}={1}&", kvPair.Key, kvPair.Value);
                }
            }
            sb.Append($"sign={sign}");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url+ sb.ToString());
            request.Method = method;
            request.Accept = "text/html";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.Headers.Add("Authorization", authorization);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return TString.DecodeUnicode(reader.ReadToEnd());
            }
        }

        public string ShiXiSengHttpPost(string url, Dictionary<string, string> paramDic)
        { //该方法不可用，通过测试发现，参数只能写在header里，不能写在body里
            long timespan = TTime.GetUnixTimestampNow();
            string sign = TMD5.GenerateMD5(appId + appSecret + timespan, Encoding.UTF8).ToUpper(); //签名
            string authorization = TString.ToBase64String(appId + ":" + timespan); //认证
            StringBuilder sb = new StringBuilder();
            sb.Append("?");
            foreach (var kvPair in paramDic)
            {
                sb.AppendFormat("{0}={1}&", kvPair.Key, kvPair.Value);
            }
            sb.Append($"sign={sign}");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + sb.ToString());

            request.Method = "POST";
            request.Accept = "text/html";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.Headers.Add("Authorization", authorization);

            StringBuilder buffer = new StringBuilder();
            buffer.Append("degree=本科");
            byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());
            request.ContentLength = data.Length;
            using (Stream requeststream = request.GetRequestStream())
            {
                requeststream.Write(data, 0, data.Length);
                requeststream.Close();
            }

            request.Method = "POST";
            request.Accept = "text/html";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.Headers.Add("Authorization", authorization);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return TString.DecodeUnicode(reader.ReadToEnd());
            }
        }

        /// <summary>
        /// MD5字符串加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns>加密后字符串</returns>
        public static string GenerateMD5(string txt)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.UTF8.GetBytes(txt);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Unicode解码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string DecodeUnicode(string s)
        {
            Regex reUnicode = new Regex(@"\\u([0-9a-fA-F]{4})", RegexOptions.Compiled);

            return reUnicode.Replace(s, m =>
            {
                short c;
                if (short.TryParse(m.Groups[1].Value, System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out c))
                {
                    return "" + (char)c;
                }
                return m.Value;
            });
        }
    }
}
