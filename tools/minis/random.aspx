<%@ Page Language="C#" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.IO" %>
<script runat="server">
    public String RandomImage()
    {
        var repo = HttpContext.Current.Server.MapPath(".");
        Random rand = new Random();
        var files = Directory.GetFiles(repo, "*.*", SearchOption.AllDirectories).Where(name => !name.EndsWith(".aspx")).ToList();
        int index = rand.Next(files.Count);
        string randomFile = files[index];
        randomFile = Path.GetFileName(randomFile);
        return randomFile;
    }
</script>
<!DOCTYPE html>
<html>
<head>
    <title><% =RandomImage() %></title>
    <style>
        html,
        body {
            height: 100%;
            margin: 0px;
            padding: 0px;
            background-color: #111111;
        }
        .site-wrapper {
            display: table;
            width: 100%;
            height: 100%; /* For at least Firefox */
            min-height: 100%;
            max-width: 100%;
            padding: 0px;
            margin: 0px;
        }
        .site-wrapper-inner {
            display: table-cell;
            vertical-align: middle;
            text-align: center;
        }
        img {
            max-width: 100%;
            max-height: 100%;
            -webkit-box-shadow: 0px 0px 45px 0px rgba(0,0,0,0.75);
            -moz-box-shadow: 0px 0px 45px 0px rgba(0,0,0,0.75);
            box-shadow: 0px 0px 45px 0px rgba(0,0,0,0.75);
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            margin: 0 auto;
            display: block;
            background-color: white;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <div class="site-wrapper"><div class="site-wrapper-inner">
        <a href="."><img src="<% =RandomImage() %>" alt="" /></a>
    </div></div>
</body>
</html>