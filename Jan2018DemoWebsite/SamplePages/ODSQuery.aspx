<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ODSQuery.aspx.cs" Inherits="Jan2018DemoWebsite.SamplePages.ODSQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>ODS Query</h3>
    <div class="row">
        <div class="col-sm-12">
        <asp:GridView ID="AlbumList" runat="server" AutoGenerateColumns="False" DataSourceID="AlbumListODS" 
            AllowPaging="True" PageSize="15"
            BorderStyle ="None"
            GridLines="Horizontal"
            CellPadding ="5" CellSpacing="10" OnSelectedIndexChanged="AlbumList_SelectedIndexChanged">

            <Columns>
                <asp:TemplateField HeaderText="Id" SortExpression="AlbumId">
                    <%--can change all the bind to eval, and doest matter--%>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("AlbumId") %>' ID="Label1"></asp:Label>&nbsp;&nbsp;&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title" SortExpression="Title">
                    
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Title") %>' ID="Label2"></asp:Label>&nbsp;&nbsp;&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Artist" SortExpression="ArtistID">
                    
                    <ItemTemplate>
                        <asp:DropDownList ID="Artistlist" runat="server" DataSourceID="ArtistListODS" DataTextField="Name" DataValueField="ArtistId" selectedvalue='<%# Bind("ArtistID") %>' Width="300px"></asp:DropDownList>
<%--                        <asp:Label runat="server" Text='<%# Bind("ArtistID") %>' ID="Label3"></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Year" SortExpression="ReleaseYear">
                    
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("ReleaseYear") %>' ID="Label4"></asp:Label>&nbsp;&nbsp;&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Label" SortExpression="ReleaseLabel">
                    
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("ReleaseLabel") %>' ID="Label5"></asp:Label>&nbsp;&nbsp;&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:CommandField SelectText="View" ShowSelectButton="True"></asp:CommandField>
            </Columns>
        </asp:GridView>
            </div>
        
        <asp:ObjectDataSource ID="AlbumListODS" runat="server" 
            OldValuesParameterFormatString="original_{0}" 
            SelectMethod="Albums_List" 
            TypeName="ChinookSystem.BLL.AlbumController">

        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="ArtistListODS" runat="server" 
            OldValuesParameterFormatString="original_{0}" 
            SelectMethod="Artists_List" 
            TypeName="ChinookSystem.BLL.ArtistController"></asp:ObjectDataSource>
    </div>
</asp:Content>
