<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
</head>
<body>
    <div>
		ViewBag.result = <%= ViewBag.result %>
		<br /><br /><br /><form name="mvcForm" method="post" action=""><input type="submit" name="SubmitBtn" value="Submit" /></form>
    </div>
</body>
</html>
