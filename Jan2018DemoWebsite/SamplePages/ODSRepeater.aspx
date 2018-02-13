<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ODSRepeater.aspx.cs" Inherits="Jan2018DemoWebsite.SamplePages.ODSRepeater" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Repeater for Nested Linq Query</h1>

    <asp:CompareValidator ID="TrackCountLimitCompare" runat="server" 
        ErrorMessage="Invalid limit value" 
        ControlToValidate="TrackCountLimit" 
        Operator="DataTypeCheck"
        Type="Integer">   

    <asp:RangeValidator ID="TrackCountLimitRange" runat="server" 
        ErrorMessage="Limit must be greater than 0"
        MinimumValue="0" MaximumValue="100"
        ControlToValidate="TrackCountLimit" Type="Integer">
    </asp:RangeValidator>
    </asp:CompareValidator>



    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />

    <asp:Label ID="Label1" runat="server" Text="Enter playlist track lower limit:"></asp:Label> &nbsp;

    <asp:TextBox ID="TrackCountLimit" runat="server"
        TextMode="Number"></asp:TextBox> &nbsp;

    <asp:Button ID="DisplayClientPlaylists" runat="server" Text="Display Client Playlists" class="btn btn-primary" />

    <br />

    <asp:Repeater ID="ClientPlaylist" runat="server" 
        DataSourceID="ODSClientPlaylist"
        ItemType="Chinook.Data.DTOs.ClientPlaylist">
        <HeaderTemplate>
            <h3>Client Playlist</h3>
        </HeaderTemplate>
        <ItemTemplate>
            <h4>
                <%# Item.playlist %>
            </h4><br /> 
            
            <asp:Repeater ID="PlaylistSongs" runat="server"
                DataSource ="<%# Item.songs %>"
                ItemType="Chinook.Data.POCOs.TracksAndGenre">
                <ItemTemplate>
                    <%# Item.songtitle %> (<%# Item.songgenre %>) <br />
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <span style="color:aqua">
                    <%# Item.songtitle %> (<%# Item.songgenre %>)
                    </span>
                </AlternatingItemTemplate>

            </asp:Repeater>

        </ItemTemplate>


    </asp:Repeater>

    <asp:ObjectDataSource ID="ODSClientPlaylist" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Playlist_ClientPlaylist" TypeName="ChinookSystem.BLL.PlaylistController">
        <SelectParameters>
            <asp:ControlParameter ControlID="TrackCountLimit" PropertyName="Text" Name="trackcountlimit" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>

