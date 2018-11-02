using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Web.Services.Description;
using System.Xml;
using System.Reflection;
using System.Xml.Serialization;

namespace Sephiroth.Infrastructure.Common.WebClient
{
    /// <summary>
    /// 
    /// </summary>
    public class XMLFunction
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            StringReader sr = new StringReader(xml);
            T obj = (T)xs.Deserialize(sr);
            sr.Close();
            sr.Dispose();
            return obj;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string Serializer<T>(T t)
        {
            XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
            xsn.Add(string.Empty, string.Empty);
            XmlSerializer xs = new XmlSerializer(typeof(T));
            StringWriter sw = new StringWriter();
            xs.Serialize(sw, t, xsn);
            string str = sw.ToString();
            sw.Close();
            sw.Dispose();
            return str;
        }
    }

    /// <summary>
    /// 动态webservices
    /// </summary>
    public class SIC_DynamicWebServices
    {
        /// <summary>
        /// 动态调用web服务  
        /// </summary>
        /// <param name="url"></param>
        /// <param name="methodname"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object InvokeWebService(string url, string methodname, object[] args)
        {
            return SIC_DynamicWebServices.InvokeWebService(url, null, methodname, args);
        }
        public static object InvokeWebService(string url, string classname, string methodname, object[] args)
        {
            string @namespace = "EnterpriseServerBase.WebService.DynamicWebCalling";
            if ((classname == null) || (classname == ""))
            {
                classname = SIC_DynamicWebServices.GetWsClassName(url);
            }

            try
            {
                //获取WSDL  
                System.Net.WebClient wc = new System.Net.WebClient();
                Stream stream = wc.OpenRead(url + "?WSDL");
                ServiceDescription sd = ServiceDescription.Read(stream);
                ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                sdi.AddServiceDescription(sd, "", "");
                CodeNamespace cn = new CodeNamespace(@namespace);

                //生成客户端代理类代码  
                CodeCompileUnit ccu = new CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);
                CSharpCodeProvider csc = new CSharpCodeProvider();
                ICodeCompiler icc = csc.CreateCompiler();

                //设定编译参数  
                CompilerParameters cplist = new CompilerParameters();
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");

                //编译代理类  
                CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
                if (true == cr.Errors.HasErrors)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                    {
                        sb.Append(ce.ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }

                //生成代理实例，并调用方法  
                System.Reflection.Assembly assembly = cr.CompiledAssembly;
                Type t = assembly.GetType(@namespace + "." + classname, true, true);
                object obj = Activator.CreateInstance(t);
                System.Reflection.MethodInfo mi = t.GetMethod(methodname);

                return mi.Invoke(obj, args);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message, new Exception(ex.InnerException.StackTrace));
            }
        }

        private static string GetWsClassName(string wsUrl)
        {
            string[] parts = wsUrl.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');

            return pps[0];
        }
    }



    /// <summary> 
    /// WebService代理类  
    /// </summary>  
    public class SIC_WebServiceAgent
    {
        private object agent;
        private Type agentType;
        private const string CODE_NAMESPACE = "Beyondbit.WebServiceAgent.Dynamic";
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public SIC_WebServiceAgent(string url)
        {
            XmlTextReader reader = new XmlTextReader(url + "?wsdl");

            //创建和格式化 WSDL 文档  
            ServiceDescription sd = ServiceDescription.Read(reader);

            //创建客户端代理代理类  
            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
            sdi.AddServiceDescription(sd, null, null);

            //使用 CodeDom 编译客户端代理类  
            CodeNamespace cn = new CodeNamespace(CODE_NAMESPACE);
            CodeCompileUnit ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);
            Microsoft.CSharp.CSharpCodeProvider icc = new Microsoft.CSharp.CSharpCodeProvider();
            CompilerParameters cp = new CompilerParameters();
            CompilerResults cr = icc.CompileAssemblyFromDom(cp, ccu);
            agentType = cr.CompiledAssembly.GetTypes()[0];
            agent = Activator.CreateInstance(agentType);
        }

        /// <summary>
        /// 调用指定的方法
        /// </summary>
        /// <param name="methodName">方法名，大小写敏感</param>
        /// <param name="args">参数，按照参数顺序赋值</param>
        /// <returns></returns>
        public object Invoke(string methodName, params object[] args)
        {
            MethodInfo mi = agentType.GetMethod(methodName);
            return this.Invoke(mi, args);
        }

        /// <summary>
        /// 调用指定的方法
        /// </summary>
        /// <param name="method">方法名</param>
        /// <param name="args">参数，按照参数顺序赋值</param>
        /// <returns></returns>
        public object Invoke(MethodInfo method, params object[] args)
        {
            return method.Invoke(agent, args);
        }

        /// <summary>
        /// 
        /// </summary>
        public MethodInfo[] Methods
        {
            get
            {
                return agentType.GetMethods();
            }
        }
    }

}
