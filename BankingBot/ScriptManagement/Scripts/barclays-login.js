(function () {
    var values = {
        surname: __$surname,
        membershipNumber: __$membershipNumber,
        cardNumber: __$cardNumber,
        sortCode: __$sortCode,
        accountNumber: __$accountNumber
    };

    console.log(values);

    $("#surname").val(values.surname);

    if (values.membershipNumber) {
        $("#membership-radio").click();
        $("#membership-num").val(values.membershipNumber);
    }
    else if (values.cardNumber) {

        $("#debitCardStep1").val("Testing");
        $("#debitCardStep1").hide();

        // Populate card number text boxes
        var cardNumberSplit = accountHelpers.splitCardNumber(values.cardNumber);
        console.log(cardNumberSplit);
        for (var i = 0; i < 4; i++) {
            $("#debitCardStep" + (i + 1)).val(cardNumberSplit[i]);
        }

        $("#card-radio").click();

    }
})();
