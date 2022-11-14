@ModelType ProntoEntries.Sale
@Code
    ViewData("Title") = "Delete"
End Code


<div class="orgcontainer create">
    <div class="titlediv">
        <h2>Remove Item</h2>
        @Html.ActionLink("Back to Cart", "Cart", "Entries")

    </div>
    <hr />

    <div style="text-align:center">
        <div style="align-items:center">
            <p>
                Removing @Html.Action("Get_ParticipantName", "Entries", New With {.Id = Model.ParticipantID}) From Cart
            </p>
        </div>       

        @Using (Html.BeginForm())
            @Html.AntiForgeryToken()

            @<div class="form-actions no-color" style="align-items:center" >
                <input type="submit" value="Confirm" class="btn btn-default" /> 
            </div>
        End Using

        
    </div>
</div>
