@ModelType IEnumerable(Of ProntoEntries.AddonItem)
@Code
    ViewData("Title") = "Index"
End Code



<div class="orgcontainer create">
    <div class="titlediv">
        <h2>Shop</h2>
        <div>
            <ul>
                <li>
                    @Html.ActionLink("Cart(" + ViewBag.CartCount.ToString() + ")", "Cart", "Entries", New With {.id = ViewBag.SelectedRace}, Nothing)
                </li>
            </ul>
        </div>
    </div>

    <hr />
    @If ViewBag.InCart = True Then
        @<div class="linkbutton" style="margin:auto;width:30%;">
    @Html.ActionLink("Cart(" + ViewBag.CartCount.ToString() + ")", "Cart", "Entries")
</div>
    End If


    <div class="raceflex">
        @For Each item In Model


            @<div Class="raceflexitem">
                <form action="" method="get" style="text-align:center">

                    @Html.Action("Get_ItemImage", New With {.id = item.RaceID})
                    <ul class="shopselecter">
                        <li>
                            @Html.DisplayFor(Function(modelItem) item.Name)
                        </li>
                        <li>
                            @Html.Action("Get_ItemSize", New With {.id = item.ItemID})
                        </li>
                        <li>
                            @Html.DropDownList("ParticipantID", Nothing, htmlAttributes:=New With {.class = "form-controlDDL"}, optionLabel:="Select User")
                        </li>
                        @If ViewBag.HasItem = True Then
                            @<li style="color:red">
                                This User Already Purchased This Item.
                            </li>
                        Else
                            @If ViewBag.ItemInCart = True Then
                                @<li style="color:red">
                                    This Selection Is In Your Cart
                                </li>
                            Else
                                @If ViewBag.NoEntry = True Then
                                    @<li style="color:red">
                                        Item Can Only Be Added To Existing Race Entries
                                    </li>
                                End If
                            End If
                        End If
                    </ul>
                    <hr />
                    <div class="button-group">
                        <input type="submit" name="submit" value="Buy" />
                    </div>
                </form>

            </div>
        Next
    </div>

</div>