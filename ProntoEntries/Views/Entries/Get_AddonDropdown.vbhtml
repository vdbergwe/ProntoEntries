@Html.DropDownList("Addon", Nothing, htmlAttributes:=New With {.class = "form-controlDDL"}, optionLabel:="")
<div class="ParticipantSelection">
    <div>
        @Html.ActionLink("New Link", "UpdateEntry", New With {.id = ViewBag.SaleID})
    </div>
</div>
