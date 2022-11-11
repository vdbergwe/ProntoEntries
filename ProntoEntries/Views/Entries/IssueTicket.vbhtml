@ModelType IEnumerable(Of ProntoEntries.Entry)

@Code
    ViewData("Title") = "Entries"
End Code

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

        body {
            border: 4px solid black;
            border-radius: 5px;
        }

        th {
            text-align: left;
            gap: 10px;
            /*width: 200px;*/
        }
    </style>
</head>
<body>

    <div class="titlediv">
        @For Each item In Model
            @<div class="ticketflextop">
                <div class="ticketflextop item" style="margin: 0 0 0 10px">
                    <h2>Entry Confirmation</h2>
                </div>
                <div class="ticketflextop item">
                    <p>
                        <strong>Ticket Number:</strong> <br />
                        @Html.DisplayFor(Function(modelItem) item.EntryID)
                    </p>                   
                </div>
                <div class="ticketflextop item">
                    <p>
                        <strong>Confirmation:</strong> <br />
                        @Html.DisplayFor(Function(modelItem) item.PayFastReference)
                    </p>                    
                </div>
            </div>
        Next

    </div>
    <hr />

    <div class="ticketflex">
        <div class="ticketflex item" style="width: 35%;">
            <img src="@Url.Content(ViewBag.Background)" style="width: 220px; margin: 0 0 0 10px" />
        </div>
        <div class="ticketflex item" >

            <table class="table">
                @*<tr>
                    <th>
                        Participant
                    </th>
                    <th style="padding: 0 0 0 10px;">
                        Event
                    </th>
                    <th style="padding: 0 0 0 10px;">
                        Selections
                    </th>
                </tr>*@

                @For Each item In Model
                    @<tr style="border: 15px solid white">
                        <td>
                            <strong>First Name:</strong> <br />
                            @Html.Action("Get_ParticipantFirstName", New With {.Id = item.ParticipantID})
                            <br />
                            <strong>Last Name:</strong> <br />
                            @Html.Action("Get_ParticipantLastName", New With {.Id = item.ParticipantID})
                            <br />
                            <strong>ID Number:</strong> <br />
                            @Html.Action("Get_ParticipantID", New With {.Id = item.ParticipantID})
                        </td>
                        <td style="padding: 0 0 0 10px;">
                            <strong>Event:</strong> <br />
                            @Html.Action("Get_RaceName", New With {.Id = item.RaceID})
                            <br />
                            <strong>Distance:</strong> <br />
                            @Html.Action("Get_Distance", New With {.Id = item.DivisionID})
                            <br />
                            <Strong>Division:</Strong> <br />
                            @Html.Action("Get_DivisionName", New With {.Id = item.DivisionID})
                        </td>
                        <td style="padding: 0 0 0 10px;">
                            <Strong>Add-ons:</Strong> <br />
                            @Html.Action("ViewAddOnsTicket", "AddonOptions", New With {.Id = item.PaymentReference, .ParticipantID = item.ParticipantID})
                            <br />
                            <br />
                        </td>
                        <td>
                        </td>
                    </tr>
                Next
            </table>
        </div>
    </div>


</body>
</html>
