@ModelType ProntoEntries.Entry

@Code
    ViewData("Title") = "Transfer"
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
                    Please confirm substitution voucher for Entry ID: @Html.DisplayFor(Function(model) model.EntryID)
                    <br />
                    <br />
                    A R100-00 admin fee will be charged.
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
