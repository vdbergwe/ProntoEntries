@ModelType ProntoEntries.Voucher


<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
    <style>
        .ticketflex {
            display: -webkit-box;
            display: -webkit-flex;
            display: flex;
            /*gap: 10px;*/
            justify-content: space-between;
            /*align-content:center;*/
        }

            .ticketflex .item {
                /*            -webkit-box-flex: 1;
                -webkit-flex: 1;
                flex: 1;*/
            }

        .ticketflextop {
            display: -webkit-box;
            display: -webkit-flex;
            display: flex;
            gap: 10px;
            justify-content: space-between;
            /*align-content:center;*/
        }

            .ticketflextop .item {
                -webkit-box-flex: 1;
                -webkit-flex: 1;
                flex: 1;
            }

        .MainTicket {
            border: 4px solid black;
            border-radius: 5px;
            min-width: 650px;
        }

        th {
            text-align: left;
            gap: 10px;
            /*width: 200px;*/
        }

        input[type="button" i] {
            display: flex;
            width: 12%;
            height: 40px;
            text-align: center;
            background: dodgerblue;
            border-radius: 6px;
            border-color: transparent;
            color: #000;
            font-family: raleway, sans-serif;
            text-transform: uppercase;
            min-width: 160px;
            margin: 15px 0 0 30%;
        }
    </style>
</head>
<body>
    <div class="MainTicket">
        <div class="ticketflextop">
            <div class="ticketflex item" style="width: 35%;">
                <img src="@Url.Content(ViewBag.Background)" style="width: 220px; margin: 0 0 0 10px" />
            </div>
            <div class="ticketflextop item" style="margin: 0 0 0 10px">
                <h2>Voucher Confirmation</h2>
            </div>
        </div>
        </hr>
        <div class="titlediv">
            <div class="ticketflextop">
                <div class="ticketflextop item">
                    <p>
                        <strong>Voucher Number:</strong> <br />
                        @Html.DisplayFor(Function(model) model.Code)
                    </p>
                </div>
                <div class="ticketflextop item">
                    <p>
                        <strong>Value:</strong> <br />
                        @Html.DisplayFor(Function(model) model.Value)
                    </p>
                </div>
                <div class="ticketflextop item">
                    <p>
                        <strong>Status:</strong> <br />
                        @Html.DisplayFor(Function(model) model.Status)
                    </p>
                </div>
            </div>
        </div>

    </div>

    @*<div class="linkbutton1">
            <input type="button" value="Print Confirmation" onclick="window.print()">
        </div>*@


</body>



</html>
