using System;
using System.Configuration;
namespace TLC.DataAccessLayer
{
    /// <summary>
    /// 数据访问层辅助类，从配置文件中获取数据访问类的类型，创建数据访问类的实例并返回。
    /// </summary>
    public class DataAccessHelper
    {
        //获取数据访问类实例的静态方法
        public static DataAccess GetDataAccess()
        {
            //从Web.Config中读取配置信息
            string dataAccessStringType = ConfigurationManager.AppSettings["DataAccessLayerType"];
            if (String.IsNullOrEmpty(dataAccessStringType))
            {
                throw (new NullReferenceException("必须在appSettings配置节添加DataAccessLayerType配置"));
            }
            else
            {
                //获取指定名称的类型
                Type dataAccessType = Type.GetType(dataAccessStringType);
                if (dataAccessType == null)
                {
                    throw (new NullReferenceException("DataAccessType没有定义"));
                }
                //获取DataAccess抽象类的类型
                Type tp = Type.GetType("TLC.DataAccessLayer.DataAccess");
                //确定dataAccessType类型可以赋值给DataAccess
                if (!tp.IsAssignableFrom(dataAccessType))
                {
                    throw (new ArgumentException("DataAccessType 不是派生自 TLC.DataAccessLayer.DataAccess "));

                }
                //创建dataAccessType类型的实例，并赋值给DataAccess类型。
                DataAccess dc = (DataAccess)Activator.CreateInstance(dataAccessType);
                return (dc);
            }
        }
    }
}
