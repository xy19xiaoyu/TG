using System.Text;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Data;
public class JsonHelper
{


    /// <summary>  
    /// DataTable转成Json   
    /// </summary>  
    /// <param name="jsonName"></param>  
    /// <param name="dt"></param>  
    /// <returns></returns>  
    public static string DatatTableToJson(DataTable dt, string jsonName)
    {
        StringBuilder Json = new StringBuilder();
        if (string.IsNullOrEmpty(jsonName))
            jsonName = dt.TableName;
        Json.Append("{\"total\":\"" + dt.Rows.Count + "\",\"" + jsonName + "\":[");
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Json.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Type type = dt.Rows[i][j].GetType();
                    Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + StringFormat(dt.Rows[i][j].ToString(), type));
                    if (j < dt.Columns.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
                Json.Append("}");
                if (i < dt.Rows.Count - 1)
                {
                    Json.Append(",");
                }
            }
        }
        Json.Append("]}");
        return Json.ToString();
    }

    /// <summary>  
    /// DataTable转成Json   
    /// </summary>  
    /// <param name="jsonName"></param>  
    /// <param name="dt"></param>  
    /// <returns></returns>  
    public static string DatatTableToJson(DataTable dt, string jsonName, int ItemCount)
    {
        StringBuilder Json = new StringBuilder();
        if (string.IsNullOrEmpty(jsonName))
            jsonName = dt.TableName;
        Json.Append("{\"total\":\"" + ItemCount + "\",\"" + jsonName + "\":[");
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Json.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Type type = dt.Rows[i][j].GetType();
                    Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + StringFormat(dt.Rows[i][j].ToString(), type));
                    if (j < dt.Columns.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
                Json.Append("}");
                if (i < dt.Rows.Count - 1)
                {
                    Json.Append(",");
                }
            }
        }
        Json.Append("]}");
        return Json.ToString();
    }
    /// <summary>  
    /// DataTable转成Json   
    /// </summary>  
    /// <param name="jsonName"></param>  
    /// <param name="dt"></param>  
    /// <returns></returns>  
    public static string DatatTableToJson1(DataTable dt, string jsonName, int ItemCount)
    {
        StringBuilder Json = new StringBuilder();
        if (string.IsNullOrEmpty(jsonName))
            jsonName = dt.TableName;
        Json.Append("[");
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Json.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Type type = dt.Rows[i][j].GetType();
                    Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + StringFormat(dt.Rows[i][j].ToString(), type));
                    if (j < dt.Columns.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
                Json.Append("}");
                if (i < dt.Rows.Count - 1)
                {
                    Json.Append(",");
                }
            }
        }
        Json.Append("]");
        return Json.ToString();
    }
    /// <summary>
    /// 集合->Json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="jsonName"></param>
    /// <param name="ItemCount"></param>
    /// <returns></returns>
    public static string ListToJson<T>(IList<T> list, string jsonName, string ItemCount)
    {
        StringBuilder Json = new StringBuilder();
        Json.Append("{\"total\":\"" + ItemCount + "\",\"" + jsonName + "\":[");
        if (list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T obj = Activator.CreateInstance<T>();
                PropertyInfo[] pi = obj.GetType().GetProperties();
                Json.Append("{");
                for (int j = 0; j < pi.Length; j++)
                {
                    Type type;
                    if (pi[j].GetValue(list[i], null) != null)
                    {
                        type = pi[j].GetValue(list[i], null).GetType();
                        //if (pi[j].Name.ToString() == "C_DATE")
                        //{
                        //    Json.Append("\"" + pi[j].Name.ToString() + "\":" + Convert.ToDateTime(pi[j].GetValue(list[i], null).ToString()).ToShortDateString());
                        //}
                        //else
                        //{
                            Json.Append("\"" + pi[j].Name.ToString() + "\":" + StringFormat(pi[j].GetValue(list[i], null).ToString(), type));
                        //}

                    }
                    else
                    {
                        Json.Append("\"" + pi[j].Name.ToString() + "\":\"\"");
                    }


                    if (j < pi.Length - 1)
                    {
                        Json.Append(",");
                    }
                }
                Json.Append("}");
                if (i < list.Count - 1)
                {
                    Json.Append(",");
                }
            }
        }
        Json.Append("]}");
        return Json.ToString();
    }
    public static string ListToJson1<T>(IList<T> list, string jsonName, string ItemCount)
    {
        StringBuilder Json = new StringBuilder();
        Json.Append("[");
        if (list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T obj = Activator.CreateInstance<T>();
                PropertyInfo[] pi = obj.GetType().GetProperties();
                Json.Append("{");
                for (int j = 0; j < pi.Length; j++)
                {
                    Type type;
                    if (pi[j].GetValue(list[i], null) != null)
                    {
                        type = pi[j].GetValue(list[i], null).GetType();
                        Json.Append("\"" + pi[j].Name.ToString() + "\":" + StringFormat(pi[j].GetValue(list[i], null).ToString(), type));

                    }
                    else
                    {
                        Json.Append("\"" + pi[j].Name.ToString() + "\":\"\"");
                    }


                    if (j < pi.Length - 1)
                    {
                        Json.Append(",");
                    }
                }
                Json.Append("}");
                if (i < list.Count - 1)
                {
                    Json.Append(",");
                }
            }
        }
        Json.Append("]");
        return Json.ToString();
    }   
    /// <summary>  
    /// 格式化字符型、日期型、布尔型  
    /// </summary>  
    /// <param name="str"></param>  
    /// <param name="type"></param>  
    /// <returns></returns>  
    private static string StringFormat(string str, Type type)
    {
        if(string.IsNullOrEmpty(str))
        {
            return "\"\"";
        }
        if (type == typeof(string))
        {
            str = String2Json(str);
            str = "\"" + str + "\"";
        }
        else if (type == typeof(DateTime))
        {
            str = "\"" + str + "\"";
        }
        else if (type == typeof(bool))
        {
            str = str.ToLower();
        }
        return str;
    } /// <summary>  
    /// 过滤特殊字符  
    /// </summary>  
    /// <param name="s"></param>  
    /// <returns></returns>  
    public static string String2Json(String s)
    {
        StringBuilder sb = new StringBuilder();
        if (s == null)
        {
            return "\"\"";
        }
        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];
            switch (c)
            {
                case '\"':
                    sb.Append("\\\""); break;
                case '\\':
                    sb.Append("\\\\"); break;
                case '/':
                    sb.Append("\\/"); break;
                case '\b':
                    sb.Append("\\b"); break;
                case '\f':
                    sb.Append("\\f"); break;
                case '\n':
                    sb.Append("\\n"); break;
                case '\r':
                    sb.Append("\\r"); break;
                case '\t':
                    sb.Append("\\t"); break;
                default:
                    sb.Append(c); break;
            }
        }
        return sb.ToString();
    }

}