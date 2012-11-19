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
<% foreach (int lanID in ((I_UI_DialogWithUser)Model).subViews_availableLanguages)
   {
       BasementHelloWorldCommonParts.UI.I_IdDescriptionPaar lan = BasementHelloWorldCommonParts.HelloWorldStructures.ViewStateManager.getViewFromViewState<BasementHelloWorldCommonParts.HelloWorldStructures.IdDescriptionPaar>(lanID);
       %>
<option <%= Model.GetSelectedAttributeForLanguage(lan.strProp_shortID) %>value="<%= lan.strProp_shortID %>"><%= lan.strProp_description%></option>
<% } //end foreach avaliableLanguages %>
</select>
<%
  if (((I_UI_DialogWithUser)Model).boolProp_isActionPossible_SelectLanguage)
   { %>
<input name="<%= DialogueModel.SetLanguageButtonName %>" type="submit" value="<%= ((I_UI_DialogWithUser)Model).strProp_actionExplanation_SelectLanguage %>" />
<% } //end if isActionPossible_SelectLanguage  %>
<% 
    
    
    if (((I_UI_DialogWithUser)Model).boolProp_greetingVisible)
   { %>
<br /><br /><%= ((I_UI_DialogWithUser)Model).strProp_greetingText%>
<%} //end if greetingVisible %>
<% 

    
if (((I_UI_DialogWithUser)Model).boolProp_isActionPossible_TellUserName)
   { %>
<br /><br />
<input name="<%= DialogueModel.reportNameTextBoxName %>" type="text" />
<input name="<%= DialogueModel.reportNameButtonName %>" type="submit" value="<%= ((I_UI_DialogWithUser)Model).strProp_actionExplanation_TellUserName %>" />
<%} //end if isActionPossible_TellUserName %>
<%

        
if (((I_UI_DialogWithUser)Model).boolProp_helloUserMessageVisible)
   { %>
<br /><br /><b><%= ((I_UI_DialogWithUser)Model).strProp_helloUserMessageText%></b>
<%} //end if helloUserMessageVisible %>

<%

if (((I_UI_DialogWithUser)Model).boolProp_isActionPossible_AnswerChatAgainQuestion)
   { %>
<br /><br /><%= ((I_UI_DialogWithUser)Model).strProp_questionForChatingAgain%>
<input name="<%= DialogueModel.chatAgainYESButtonName %>" type="submit" value="<%= ((I_UI_DialogWithUser)Model).strProp_actionExplanation_DoChatAgain %>" />
<input name="<%= DialogueModel.chatAgainNOButtonName %>" type="submit" value="<%= ((I_UI_DialogWithUser)Model).strProp_actionExplanation_DontChatAgain %>" />

<%} //end if isActionPossible_AnswerChatAgainQuestion %>

<% } //End using Html.BeginForm%>
</div>
</body>
</html>