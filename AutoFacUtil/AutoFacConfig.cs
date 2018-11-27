using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Autofac.Integration.WebApi;
using Autofac;
using System.Web.Http;

namespace Sephiroth.Infrastructure.Common.AutoFacUtil
{
    /// <summary>
    /// CLR 版本：       4.0.30319.42000
    /// 类 名 称：       AutoFacConfig
    /// 机器名称：       102F
    /// 命名空间：       Sephiroth.Infrastructure.Common.AutoFacUtil
    /// 文 件 名：       AutoFacConfig
    /// 创建时间：       2018/11/27 上午10:41:41
    /// 作    者：       吴占超
    /// 说    明：       初始化依赖注入
    /// 修改时间：
    /// 修 改 人：
    /// </summary>
    public class AutoFacConfig
    {
        /// <summary>
        /// 注册
        /// </summary>
        public static AutofacWebApiDependencyResolver Register()
        {
            ContainerBuilder builder = new Autofac.ContainerBuilder();

            // Assembly controllerAss = Assembly.Load(string.Format("{0}.Web", proname));

            // builder.RegisterControllers(Assembly.GetExecutingAssembly());
            // builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            Assembly repositoryAss = Assembly.Load("serviceimpl");

            Type[] rtypes = repositoryAss.GetTypes();

            builder.RegisterTypes(rtypes)
                .AsImplementedInterfaces();

            // builder.RegisterTypes(typeof(SIC_WebClient)).SingleInstance().PropertiesAutowired();

            var container = builder.Build();

            // DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            // Set the dependency resolver for Web API.
            return new AutofacWebApiDependencyResolver(container);
            //GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;

        }
    }
}
