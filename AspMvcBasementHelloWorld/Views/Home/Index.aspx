<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<AspMvcBasementHelloWorld.ViewModels.DialogueModel>" %>
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
<% using (Html.BeginForm())
   {%>
<select name="<%= DialogueModel.languageDropDownName %>">
<option <%= Model.GetSelectedAttributeForLanguage("") %>value=""></option>
<% foreach (BasementHelloWorldCommonParts.UI.I_IdDescriptionPaar lan in ((I_UI_DialogWithUser)Model).avaliableLanguages)
   { %>
<option <%= Model.GetSelectedAttributeForLanguage(lan.shortID) %>value="<%= lan.shortID %>"><%= lan.description%></option>
<% } //end foreach avaliableLanguages %>
</select>
<% if (((I_UI_DialogWithUser)Model).isActionPossible_SelectLanguage)
   { %>
<input name="<%= DialogueModel.SetLanguageButtonName %>" type="submit" value="<%= ((I_UI_DialogWithUser)Model).actionExplanation_SelectLanguage %>" />
<% } //end if isActionPossible_SelectLanguage  %>
<% if (((I_UI_DialogWithUser)Model).greetingVisible)
   { %>
<br /><br /><%= ((I_UI_DialogWithUser)Model).greetingText%>
<%} //end if greetingVisible %>
<% 

    
if (((I_UI_DialogWithUser)Model).isActionPossible_TellUserName)
   { %>
<br /><br />
<input name="<%= DialogueModel.reportNameTextBoxName %>" type="text" />
<input name="<%= DialogueModel.reportNameButtonName %>" type="submit" value="<%= ((I_UI_DialogWithUser)Model).actionExplanation_TellUserName %>" />
<%} //end if isActionPossible_TellUserName %>
<%

        
if (((I_UI_DialogWithUser)Model).helloUserMessageVisible)
   { %>
<br /><%= ((I_UI_DialogWithUser)Model).helloUserMessageText %>
<%} //end if helloUserMessageVisible %>


<% } //End using Html.BeginForm%>
</div>
</body>
</html>