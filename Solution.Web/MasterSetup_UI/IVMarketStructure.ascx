<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IVMarketStructure.ascx.cs" Inherits="MasterSetup_UI_IVMarketStructure" %>


<div style="padding-top: 6px;"></div>

<div class="form-group row">
    <label for="GroupSelect" runat="server" id="groupLabel" class="col-sm-2 col-form-label">Group:  </label>

    <div class="col-sm-3">
        <div class="input-group">
            <asp:DropDownList runat="server" ID="GroupSelect" AutoPostBack="true" OnSelectedIndexChanged="GroupSelect_SelectedIndexChanged" class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>
            <asp:HiddenField ID="hfMarket" runat="server" />
            <asp:HiddenField ID="hfSubTeritory" runat="server" />
            <asp:HiddenField ID="hfTeritory" runat="server" />
            <asp:HiddenField ID="hfArea" runat="server" />
            <asp:HiddenField ID="hfZone" runat="server" />
            <asp:HiddenField ID="hfGroupId" runat="server" />

            <span class="input-group-text text-c-red">*</span>

        </div>
    </div>


    <label for="ZoneSelect" runat="server" id="zoneLabel" class="col-sm-2 col-form-label">Zone:  </label>

    <div class="col-sm-3">
        <div class="input-group">
            <asp:DropDownList runat="server" ID="ZoneSelect" AutoPostBack="true" OnSelectedIndexChanged="ZoneSelect_SelectedIndexChanged" class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>




        </div>

    </div>

</div>





<div class="form-group row" style="margin-top: 6px;">
    <label class="col-sm-2 col-form-label" runat="server" id="regionLabel">Region:  </label>

    <div class="col-sm-3">
        <div class="input-group">
            <asp:DropDownList runat="server" ID="AreaSelect" AutoPostBack="true" OnSelectedIndexChanged="AreaSelect_SelectedIndexChanged" class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>



        </div>
    </div>




    <label for="AreaSelect" class="col-sm-2 col-form-label" runat="server" id="areaLabel">Area:  </label>

    <div class="col-sm-3">

        <div class="input-group">
            <asp:DropDownList runat="server" ID="TeritorySelect" AutoPostBack="true" OnSelectedIndexChanged="TeritorySelect_SelectedIndexChanged" class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>

            <span id="v-TeritorySelect" class="invalid-tooltip fade hide" data-delay="2000"></span>


        </div>
    </div>

</div>



<div class="form-group row" style="margin-top: 6px;">

    <label for="MarketSelect" class="col-sm-2 col-form-label" runat="server" id="territoryLabel">Territory:  </label>

    <div class="col-sm-3">

        <div class="input-group">
            <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="SubTeritory_SelectedIndexChanged" ID="SubTeritory" class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>



        </div>

    </div>
    <label for="MarketSelect" class="col-sm-2 col-form-label" runat="server" id="marketLabel">Market:  </label>

    <div class="col-sm-3">

        <div class="input-group">
            <asp:DropDownList runat="server" ID="MarketSelect" class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>



        </div>

    </div>
</div>
