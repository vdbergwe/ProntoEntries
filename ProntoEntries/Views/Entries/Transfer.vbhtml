@ModelType ProntoEntries.Entry

@Code

End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Issue Subtitute Voucher</title>
</head>
<body>


    <div class="orgcontainer create">
        <div class="titlediv">
            <h2>Substitution</h2>
            @Html.ActionLink("Back to Entries", "Index", "Entries")

        </div>
        <hr />

        <div style="text-align:center">
            <div style="align-items:center">
                <p style="color:white;">
                    Please Confirm Substitution Voucher For Entry ID: @Html.DisplayFor(Function(model) model.EntryID)
                    <br />
                    A R100-00 admin fee Will Be Charged.
                </p>
            </div>

            @Using (Html.BeginForm())
                @Html.AntiForgeryToken()

                @<div class="form-actions no-color" style="align-items:center">
                    <input type="submit" value="Confirm" class="btn btn-default" />
                </div>
            End Using


        </div>
    </div>

</body>
</html>
