var Rate = {
    RateObj: {},
    OpenRatePopUp: function () {
        ShowModal.show("ModalRateVisit");
        Rate.IntPlugin();
    },
    IntPlugin: function () {
        $(".RateVisitInput").starRating({
            totalStars: 5,
            starShape: 'rounded',
            starSize: 20,
            emptyColor: '#6b6c6d',
            hoverColor: '#fe2d81',
            activeColor: '#fe2d81',
            ratedColor: '#fe2d81',
            useGradient: false,
            forceRoundUp: true,
            callback: function (currentRating, $el) {
                Rate.RateObj[$($el).attr("RateFactor")] = currentRating;
            }
        });
    },
    SubmitRate: function () {

        var ClientID = JSON.parse(localStorage.getItem("UserData")) == null ? null : JSON.parse(localStorage.getItem("UserData")).ClientID;

        var VisitID = JSON.parse(localStorage.getItem("CurrentVisit")) == null ? null : JSON.parse(localStorage.getItem("CurrentVisit")).CurrentVisitID;

       // Rate.RateObj[Notes] = $("#RateNotes").val();
        var ListFeedBackObj = [];
        if (Rate.RateObj.Agents !== null && ClientID !== null) {
            var  FeedBackObjAgents = {
                ClientID: ClientID,
                ClientVisitID: VisitID,
                RatingScale: Rate.RateObj.Agents,
                ServiceID: 1
            };
            ListFeedBackObj.push(FeedBackObjAgents);

        }
        if (Rate.RateObj.Hotel !== null && ClientID !== null) {
            var FeedBackObjHotel = {
                ClientID: ClientID,
                ClientVisitID: VisitID,
                RatingScale: Rate.RateObj.Hotel,
                ServiceID: 2
            };
            ListFeedBackObj.push(FeedBackObjHotel);

        }

        if (Rate.RateObj.Flight !== null && ClientID !== null) {
            var FeedBackObjFlight = {
                ClientID: ClientID,
                ClientVisitID: VisitID,
                RatingScale: Rate.RateObj.Flight,
                ServiceID: 3
            };
            ListFeedBackObj.push(FeedBackObjFlight);

        }


        if (Rate.RateObj.Trips !== null && ClientID !== null) {
            var FeedBackObjTrips = {
                ClientID: ClientID,
                ClientVisitID: VisitID,
                RatingScale: Rate.RateObj.Trips,
                ServiceID: 4
            };
            ListFeedBackObj.push(FeedBackObjTrips);

        }


        if (Rate.RateObj.Country !== null && ClientID !== null) {
            FeedBackObjCountry = {
                ClientID: ClientID,
                ClientVisitID: VisitID,
                RatingScale: Rate.RateObj.Country,
                ServiceID: 5
            };
            ListFeedBackObj.push(FeedBackObjCountry);

        }

        if (Rate.RateObj.Location !== null && ClientID !== null) {
            var FeedBackObjLocation = {
                ClientID: ClientID,
                ClientVisitID: VisitID,
                RatingScale: Rate.RateObj.Location,
                ServiceID: 6
            };
            ListFeedBackObj.push(FeedBackObjLocation);

        }


        if (Rate.RateObj.PickupProcess !== null && ClientID !== null) {
            var FeedBackObjPickupProcess = {
                ClientID: ClientID,
                ClientVisitID: VisitID,
                RatingScale: Rate.RateObj.PickupProcess,
                ServiceID: 7
            };
            ListFeedBackObj.push(FeedBackObjPickupProcess);

        }

        if (Rate.RateObj.EMAServices !== null && ClientID !== null) {
            var FeedBackObjEMAServices = {
                ClientID: ClientID,
                ClientVisitID: VisitID,
                RatingScale: Rate.RateObj.EMAServices,
                ServiceID: 8,
                ClientComment: $("#RateNotes").val()
            };
            ListFeedBackObj.push(FeedBackObjEMAServices);

        }
        var urlAddFeedBack = GlobalResourses.BaseURL + "Client/AddFeedBack";
        Common.Ajax('Post', urlAddFeedBack, JSON.stringify(ListFeedBackObj), 'json', SucessAddFeedBack, false);
        localStorage.removeItem("CurrentVisit");
        window.location.href = ("Homepage.html");

        //Trips: 4.5

    }
};

function SucessAddFeedBack() {



}