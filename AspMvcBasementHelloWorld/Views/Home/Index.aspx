<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<AspMvcBasementHelloWorld.ViewModels.MultiDialogueModel>" %>
<%@ Import Namespace="BasementHelloWorldCommonParts.UI" %>
<%@ Import Namespace="AspMvcBasementHelloWorld.ViewModels" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
</head>
<body>
<div>
<b><a href="/Home/links">Step by Step</a></b> links collection shows the evolution of the Application<br />
<br />
<b><a href="/MultiD">Current Development</a></b> - the feature currently being developed.<br />
<br />
<b>Last Working Version</b> - below is the last working and tested verson.<br />
<br />
</div>
    <div>
<% using (Html.BeginForm())
{%>

<% foreach (int dlgID in ((I_UI_MultipleDialogs)Model).subViews_DialogWithUser)
   {
       Mock_UI_DialogWithUser dlg = BasementHelloWorldCommonParts.HelloWorldStructures.ViewStateManager.getViewFromViewState<Mock_UI_DialogWithUser>(dlgID);
       %>
<% Html.RenderPartial("singleDialog", dlg); %>
<hr />
<% } //end foreach DialogWithUser %>
<br />
<%
if (((I_UI_MultipleDialogs)Model).boolProp_isActionPossible_AddNewDialog)
   { %>
<input name="<%= MultiDialogueModel.AddNewLanguageButtonName %>" type="submit" value="+" />
<%} //end if isActionPossible_AddNewDialog %>
<%
if (((I_UI_MultipleDialogs)Model).boolProp_isActionPossible_RemoveLastDialog)
   { %>
<input name="<%= MultiDialogueModel.RemoveOldLanguageButtonName %>" type="submit" value="-" />
<%} //end if isActionPossible_RemoveLastDialog %>

<% } //End using Html.BeginForm%>
    </div>
</body>
</html>