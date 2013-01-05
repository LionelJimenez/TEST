<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyPosts.aspx.cs" Inherits="ServicesExchange.MyPosts" %>
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
                    <a style="margin-bottom: 20px; margin-left: 20px;
                        float: left; color: Black; text-decoration: none; border: 1px solid;" href="AddPost.aspx" >ADD A NEW POST</a>
                </div>
                <div style="margin-top: 250px;">
                    <a style="margin-bottom: 20px; margin-left: 20px;
                        float: left; color: Black; text-decoration: none; border: 1px solid;" href="#" >MANAGE MY POSTS</a>
                </div>
            </div>
            <div style="background-color: White; min-height: 550px; width: 700px; float: right;
                border: 1px solid;">
                <asp:Panel ID="PnlManagePost" runat="server" Style="margin-top: 20px;">
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
                
            </div>
        </div>
        <script type="text/javascript">
            function SetddlstValue() {
                document.getElementById("hiddenddlbCat").value = document.getElementById("ddlbCat").value;
            }
        </script>
    </form>
</body>
</html>
