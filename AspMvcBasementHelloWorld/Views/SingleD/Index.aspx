<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<AspMvcBasementHelloWorld.ViewModels.SingleDialogueModelHelper>" %>
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
<select name="<%= SingleDialogueModelHelper.languageDropDownName %>">
<option <%= SingleDialogueModelHelper.GetSelectedAttributeForLanguage("", Model.strProp_selectedLanguage ) %> value=""></option>

<% foreach (int lanID in ((I_UI_DialogWithUser)Model).subViews_availableLanguages)
   {
       BasementHelloWorldCommonParts.UI.I_IdDescriptionPaar lan = BasementHelloWorldCommonParts.HelloWorldStructures.ViewStateManager.getViewFromViewState<BasementHelloWorldCommonParts.HelloWorldStructures.IdDescriptionPaar>(lanID);
       %>
<option <%= SingleDialogueModelHelper.GetSelectedAttributeForLanguage(lan.strProp_shortID,  Model.strProp_selectedLanguage) %>value="<%= lan.strProp_shortID %>"><%= lan.strProp_description%></option>
<% } //end foreach avaliableLanguages %>
</select>
<%
  if (((I_UI_DialogWithUser)Model).boolProp_isActionPossible_SelectLanguage)
   { %>
<input name="<%= SingleDialogueModelHelper.SetLanguageButtonName %>" type="submit" value="<%= ((I_UI_DialogWithUser)Model).strProp_actionExplanation_SelectLanguage %>" />
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
<input name="<%= SingleDialogueModelHelper.reportNameTextBoxName %>" type="text" />
<input name="<%= SingleDialogueModelHelper.reportNameButtonName %>" type="submit" value="<%= ((I_UI_DialogWithUser)Model).strProp_actionExplanation_TellUserName %>" />
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
<input name="<%= SingleDialogueModelHelper.chatAgainYESButtonName %>" type="submit" value="<%= ((I_UI_DialogWithUser)Model).strProp_actionExplanation_DoChatAgain %>" />
<input name="<%= SingleDialogueModelHelper.chatAgainNOButtonName %>" type="submit" value="<%= ((I_UI_DialogWithUser)Model).strProp_actionExplanation_DontChatAgain %>" />

<%} //end if isActionPossible_AnswerChatAgainQuestion %>

<% } //End using Html.BeginForm%>
</div>
</body>
</html>