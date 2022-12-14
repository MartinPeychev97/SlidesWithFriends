export function changeBackground(url, { presentationId, background }) {
    $.ajax({
        type: 'PUT',
        url: url,
        dataType: "json",
        data: {
            presentationId: presentationId,
            background:background
        },
        success: function () {
            console.log('testing...')
        },
        error: function (req,status,error) {
            console.log(error.message)
        }
    })
}