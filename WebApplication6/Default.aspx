﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication6.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Untitled Document</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color: #E7EBF2;">
    <form id="Form1" runat="server">
    <div id="wrapper100" style="background-color: #E7EBF2;">
        <div id="Total-Top" style="height: 150px; margin-left: 58px;">
            <div id="Navigation" style="float: right; padding-bottom: 10px; margin-top: 35px;">
                <div style="padding-top: 20px; padding-bottom: 20px; padding-left: 5px; padding-right: 5px;
                    border: 1px solid;">
                    <div style="height: 20px;">
                        <span id="presentation1">SERVICES AND SKILLS EXCHANGE </span>
                    </div>
                </div>
            </div>
        </div>
        <div id="DownZone" style="width: 900px; display: block;">
            <div style="background-color: White; min-height: 150px; width: 190px; border: 1px solid;
                float: left;">
                <div style="margin-top: 20px;">
                    <asp:Label ID="Label2" runat="server" Style="margin-left: 20px; float: left;">MOT CLE:</asp:Label>
                    <asp:TextBox ID="txtbxSearchArticle" runat="server" Style="margin-left: 20px; float: left;
                        margin-top: 10px; width: 150px;"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label1" runat="server" Style="margin-left: 20px; float: left; margin-top: 20px;">CATEGORIE:</asp:Label>
                    <br />
                    <input type="hidden" runat="server" id="hiddenddlbCat" />
                    <asp:DropDownList ID="ddlbCat" onchange="SetddlstValue();" runat="server" Style="margin-left: 20px;
                        float: left; margin-top: 10px; width: 150px; color: Black;">
                    </asp:DropDownList>
                    <input type="hidden" runat="server" id="hidden1" />
                    <asp:LinkButton ID="LnkBtnValiderRech" OnClick="SearchArticle" runat="server" Style="color: Black;
                        text-decoration: none; margin-left: 20px; float: left; margin-top: 20px; border: 1px solid;">VALIDER</asp:LinkButton>
                </div>
                <div style="margin-top: 250px;">
                    <asp:LinkButton runat="server" ID="Span3" Style="margin-bottom: 20px; margin-left: 20px;
                        float: left; color: Black; text-decoration: none; border: 1px solid;" OnClick="PostSelection">ADD A NEW POST</asp:LinkButton>
                </div>
                <div style="margin-top: 250px;">
                    <asp:LinkButton runat="server" ID="LnkBtnEdit" Style="margin-bottom: 20px; margin-left: 20px;
                        float: left; color: Black; text-decoration: none; border: 1px solid;" OnClick="AccessMyPosts">MANAGE MY POSTS</asp:LinkButton>
                </div>
            </div>
            <div style="background-color: White; min-height: 550px; width: 700px; float: right;
                border: 1px solid;">
                <asp:Panel ID="PnlViewSearch" runat="server" Style="margin-bottom: 20px;">
                    <div style="margin-top: 20px; margin-left: 20px; text-decoration: underline;">
                        <span id="Span2">LASTEST POSTS</span>
                    </div>
                    <asp:Repeater ID="RepeatPosts" runat="server">
                        <ItemTemplate>
                            <div style="margin-top: 20px; margin-left: 20px;">
                                <div style="height: 40px;">
                                    <span style="float: left; text-decoration: underline;">
                                        <%# Eval("Categorie") %></span>
                                </div>
                                <div>
                                    <textarea runat="server" style="border: 1px solid grey;" id="DescriptionAcceuil" cols="60"
                                        rows="4"><%# Eval("_Post") %></textarea></div>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
                <asp:Panel ID="PnlAddPost" runat="server" Style="margin-top: 20px;" Visible="false">
                    <div>
                        <div style="margin-left: 20px; text-decoration: underline;">
                            <span id="Span1">NOUVELLE ANNONCE</span>
                        </div>
                        <div style="margin-top: 20px; height: 30px;">
                            <asp:Label ID="sPost" runat="server" Style="float: left; margin-left: 20px">Votre annonce *:</asp:Label>
                        </div>
                        <textarea runat="server" style="margin-bottom: 20px; margin-left: 20px; float: left;
                            border: 1px solid grey;" id="Post" cols="60" rows="4"></textarea>
                    </div>
                    <div style="width: 392px; float: left; margin-bottom: 20px;">
                        <asp:Label ID="sCategorie" runat="server" Style="margin-left: 20px; float: left;
                            margin-top: 20px;">Categorie *:</asp:Label>
                        <br />
                        <input type="hidden" runat="server" id="hiddenddlbCat1" />
                        <asp:DropDownList ID="ddlbCat1" onchange="SetddlstValue1();" runat="server" Style="margin-left: 20px;
                            float: left; width: 150px; color: Black;">
                        </asp:DropDownList>
                    </div>
                    <div style="width: 396px; margin-left: 20px;">
                        <input type="hidden" id="RegexMail" runat="server" value=".*@.{2,}\..{2,}" />
                        <asp:Label ID="sLogin" runat="server" Style="margin-right: 47px;">Login *: </asp:Label>
                        <input type="text" runat="server" id="Login" style="margin-top: 0px; margin-left: 10px;
                            width: 200px;" />
                        <span>(Votre Email)</span>
                        <br />
                        <br />
                    </div>
                    <div style="width: 353px; margin-left: 20px;">
                        <asp:Label ID="sPass" runat="server">Mot de passe *: </asp:Label>
                        <input type="text" runat="server" id="Pass" style="margin-top: 0px; margin-left: 10px;
                            width: 150px;" />
                        <span>(6 caractères)</span>
                        <br />
                        <br />
                    </div>
                    <div id="MsgErOblFld" runat="server" style="width: 345px; margin-left: 20px;">
                        <asp:Label ID="Label3" runat="server" Style="float: left;">*Champs obligatoires</asp:Label>
                    </div>
                    <asp:Panel Visible="false" ID="MsgErLogPass" runat="server" Style="width: 345px;
                        margin-left: 20px;">
                        <asp:Label ID="Label6" runat="server" Style="float: left;" ForeColor="Red">Combinaison Login-Password non valide!</asp:Label>
                    </asp:Panel>
                    <div style="float: right; margin-right: 175px;">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="AddNewPost">GO</asp:LinkButton>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PnlEditPost" runat="server" Style="margin-top: 20px;" Visible="false">
                    <div>
                        <div style="margin-left: 20px; text-decoration: underline;">
                            <span id="Span1EditPost">EDITER ANNONCE</span>
                        </div>
                        <div style="margin-top: 20px; height: 30px;">
                            <asp:Label ID="sPostEditPost" runat="server" Style="float: left; margin-left: 20px">Votre annonce *:</asp:Label>
                        </div>
                        <textarea runat="server" style="margin-bottom: 20px; margin-left: 20px; float: left;
                            border: 1px solid grey;" id="PostEditPost" cols="60" rows="4"></textarea>
                    </div>
                    <div style="width: 392px; float: left; margin-bottom: 20px;">
                        <asp:Label ID="sCategorieEditPost" runat="server" Style="margin-left: 20px; float: left;
                            margin-top: 20px;">Categorie *:</asp:Label>
                        <br />
                        <input type="hidden" runat="server" id="hiddenddlbCat1EditPost" />
                        <asp:DropDownList ID="ddlbCat1EditPost" onchange="SetddlstValue2();" runat="server"
                            Style="margin-left: 20px; float: left; width: 150px; color: Black;">
                        </asp:DropDownList>
                    </div>
                    <div id="Div1" runat="server" style="width: 345px; margin-left: 20px;">
                        <asp:Label ID="Label5" runat="server" Style="float: left;">*Champs obligatoires</asp:Label>
                        <asp:Label runat="server" ID="hiddenIdEditPostUser" Visible="false"></asp:Label>
                        <asp:Label runat="server" ID="hiddenIdEditPost" Visible="false"></asp:Label>
                    </div>
                    <div style="float: right; margin-right: 175px;">
                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="UpdatePost">VALID CHANGES</asp:LinkButton>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PnlAccessMyPosts" runat="server" Style="margin-top: 20px;" Visible="false">
                    <div style="margin-top: 20px; margin-left: 20px; text-decoration: underline;">
                        <span id="Span9">MY POSTS</span>
                    </div>
                    <div style="width: 396px; margin-left: 20px; margin-top: 50px;">
                        <input type="hidden" id="RegexMailAccessMyPosts" runat="server" value=".*@.{2,}\..{2,}" />
                        <asp:Label ID="sLoginAccessMyPosts" runat="server" Style="margin-right: 47px;">Login *: </asp:Label>
                        <input type="text" runat="server" id="LoginAccessMyPosts" style="margin-top: 0px;
                            margin-left: 10px; width: 200px;" />
                        <span>(Votre Email)</span>
                        <br />
                        <br />
                    </div>
                    <div style="width: 353px; margin-left: 20px;">
                        <asp:Label ID="sPassAccessMyPosts" runat="server">Mot de passe *: </asp:Label>
                        <input type="text" runat="server" id="PassAccessMyPosts" style="margin-top: 0px;
                            margin-left: 10px; width: 150px;" />
                        <span>(6 caractères)</span>
                        <br />
                        <br />
                    </div>
                    <div id="MsgErOblFldAccessMyPosts" runat="server" style="width: 345px; margin-left: 20px;">
                        <asp:Label ID="LblChampsOblAccMyPost" runat="server" Style="float: left;">*Champs obligatoires</asp:Label>
                    </div>
                    <asp:Panel Visible="false" ID="MsgErLogPassAccessMyPosts" runat="server" Style="width: 345px;
                        margin-left: 20px;">
                        <asp:Label ID="Label4" runat="server" Style="float: left;" ForeColor="Red">Combinaison Login-Password non valide!</asp:Label>
                    </asp:Panel>
                    <div style="float: right; margin-right: 175px;">
                        <asp:LinkButton ID="LnkBtnAccessMyPosts" runat="server" OnClick="ManagePosts">GO</asp:LinkButton>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PnlManagePost" runat="server" Style="margin-top: 20px;" Visible="false">
                    <div style="margin-top: 20px; margin-left: 20px; text-decoration: underline;">
                        <span id="Span8">MY POSTS</span>
                    </div>
                    <asp:Repeater ID="RptrMyPosts" runat="server">
                        <ItemTemplate>
                            <div style="margin-top: 20px; margin-left: 20px;">
                                <div style="height: 40px;">
                                    <span style="float: left; text-decoration: underline;">
                                        <%# Eval("Categorie") %></span>
                                </div>
                                <div>
                                    <textarea runat="server" style="border: 1px solid grey;" id="Description" cols="60"
                                        rows="4"><%# Eval("_Post") %></textarea></div>
                                <div style="float: right; margin-right: 90px;">
                                    <asp:Button runat="server" ID="BtnEdit" OnClick="EditPost" Text="Edit" />
                                    <asp:Button runat="server" ID="BtnDelete" OnClick="DeletePost" Text="Delete" />
                                    <asp:Label ID="LblID" Text='<%# Eval("Id") %>' runat="server" Visible="false" />
                                </div>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
                <asp:Panel ID="PnlValidMail" runat="server" Style="margin-top: 20px;" Visible="false">
                    <div style="margin-left: 20px; font-weight: bolder;">
                        <span id="Span4">MERCI VOTRE ADRESSE MAIL A BIEN ETE VALIDEE</span>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PnlGoodPost" runat="server" Style="margin-top: 20px;" Visible="false">
                    <div style="margin-left: 20px; font-weight: bolder;">
                        <span id="Span5">VOTRE ANNONCE A ETE ENREGISTREE AVEC SUCCES</span>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PnlBadAfterConf" runat="server" Style="margin-top: 20px;" Visible="false">
                    <div style="margin-left: 20px; font-weight: bolder;">
                        <span id="Span6">VOUS ALLEZ RECEVOIR UN E-MAIL AFIN DE VALIDER VOTRE COMPTE.</span>
                        <br />
                        <span id="Span7">VOTRE ANNONCE SERA ENSUITE VISIBLE</span>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <script type="text/javascript">
            function SetddlstValue() {
                document.getElementById("hiddenddlbCat").value = document.getElementById("ddlbCat").value;
            }
            function SetddlstValue1() {
                document.getElementById("hiddenddlbCat1").value = document.getElementById("ddlbCat1").value;
            }
            function SetddlstValue2() {
                document.getElementById("hiddenddlbCat1EditPost").value = document.getElementById("ddlbCat1EditPost").value;
            } 
        </script>
    </form>
</body>
</html>
