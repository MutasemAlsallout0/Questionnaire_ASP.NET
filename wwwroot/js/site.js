let x = 1;
function addClaimFunc() {
    console.log(x);
    $('#claimsGroup').append(`
                <div id="claim${x}" class="row">
                    <div class="col">
                        <input name="Claims" type="text" placeholder="enter claim name" class="form-control m-1" />
                    </div>
                    <div class="col-auto p-0">
                        <button id="removeClaim" type="button" class="form-control m-1 text-danger" onclick="removeClaimFunc(${x})">Remove</button>
                    </div>
                </div>`);
    x++;
}
function removeClaimFunc(index) {
    console.log(index);
    $(`#claim${index}`).remove();
}