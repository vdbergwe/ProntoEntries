@ModelType ProntoEntries.Entry

@Code

End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Revoke Voucher</title>
</head>
<body>


    <div class="orgcontainer create">
        <div class="titlediv">
            <h2>Revoke Voucher</h2>
            @Html.ActionLink("Back to List", "Index", "Vouchers")

        </div>
        <hr />

        <div style="text-align:center">
            <div style="align-items:center">
                <p style="color:white;">
                    Upgrade / Downgrade Voucher @Html.DisplayFor(Function(model) model.EntryID)
                </p>
            </div>

            @Using (Html.BeginForm())
                @Html.AntiForgeryToken()

                @<div class="form-actions no-color" style="align-items:center">
                    <input type="submit" value="Delete" class="btn btn-default" />
                </div>
            End Using


        </div>
    </div>

</body>
</html>
