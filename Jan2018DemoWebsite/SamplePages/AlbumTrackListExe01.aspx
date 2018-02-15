<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AlbumTrackListExe01.aspx.cs" Inherits="Jan2018DemoWebsite.SamplePages.AlbumTrackListExe01" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Repeater for AlbumTrack</h1>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Album_GetTrackList" TypeName="ChinookSystem.BLL.AlbumTrackListLINQController"></asp:ObjectDataSource>
    
    <asp:Repeater ID="Repeater1" runat="server" 
        DataSourceID="ObjectDataSource1"
        ItemType ="Chinook.Data.DTOs.AlbumTrack">
        <HeaderTemplate>
            <h2>Something Header</h2>
        </HeaderTemplate>
        <ItemTemplate>
            <div class="row">
                <div class ="col-md-5">
                    <%#Item. AlbumTitle%>(<%#Item.ArtistName%>)
                    <asp:GridView ID="GridView1" runat="server" DataSource="<%#Item.TrackList %>"></asp:GridView>
                </div>
            
             
                <div class ="col-md-5">
                    <asp:ListView ID="ClientListLV" runat="server"
                        DataSource="<%#Item.TrackList %>">
                        <ItemTemplate>
                             <tr style="background-color: #DCDCDC; color: #000000;">
                                <td>
                                    <asp:Label Text='<%# Eval("TrackName") %>' runat="server" ID="ClientLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("length") %>' runat="server" ID="PhoneLabel" /></td>                 
                             </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                            <tr runat="server" style="background-color: #DCDCDC; color: #000000;">
                                                <th runat="server">TrackName</th>
                                                <th runat="server">length</th>                                            
                                            </tr>
                                            <tr runat="server" id="itemPlaceholder"></tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" style="text-align: center; background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;">
                                        <asp:DataPager runat="server" ID="DataPager1">
                                            <Fields>
                                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True"></asp:NextPreviousPagerField>
                                            </Fields>
                                        </asp:DataPager>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>
                </div>
            </div>
        </ItemTemplate>


    </asp:Repeater>

</asp:Content>
