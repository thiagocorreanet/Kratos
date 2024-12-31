function postModalData(){
    const name = document.getElementById("NameInput").value;

    const data = {
        name: name
    };

    fetch("/typedata/add", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })
        .then(response => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error("Erro ao salvar os dados");
            }
        })
        .then(result => {
            if (result.success === true) {

                const modalSuccess = new bootstrap.Modal(document.getElementById('modal-success'));
                modalSuccess.show();

                document.getElementById('btn-reload').addEventListener('click', function () {
                    location.reload();
                });
            } else {

                const modalDanger = new bootstrap.Modal(document.getElementById('modal-danger'));
                modalDanger.show();

                document.getElementById('btn-reload').addEventListener('click', function () {
                    location.reload();
                });
            }
        })
        .catch(error => {
            console.error("Erro:", error);

            const modalDanger = new bootstrap.Modal(document.getElementById('modal-danger'));
            modalDanger.show();

            document.getElementById('btn-reload').addEventListener('click', function () {
                location.reload();
            });
        });
}

function putData() {
    const id = document.getElementById('Id').value.trim();
    const name = document.getElementById('Name').value.trim();

    const data = {
        id: id,
        name: name
    };

    fetch("/alterar-tipo/" + id , {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })
        .then(response => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error("Erro ao atualizar os dados");
            }
        })
        .then(result => {
            if (result.success === true) {
                const modalSuccess = new bootstrap.Modal(document.getElementById('modal-success'));
                modalSuccess.show();

                document.getElementById('btn-reload').addEventListener('click', function () {
                    window.location.href = `/consultar-tipo/${id}`;
                });
            } else {

                const modalDanger = new bootstrap.Modal(document.getElementById('modal-danger'));
                modalDanger.show();
            }
        })
        .catch(error => {
            console.error("Erro:", error);


            const modalDanger = new bootstrap.Modal(document.getElementById('modal-danger'));
            modalDanger.show();
        });
}

function deleteData() {
    const id = document.getElementById('Id').value.trim();

    fetch("/excluir-tipo/" + id, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json"
        }
    })
        .then(response => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error("Erro ao excluir os dados");
            }
        })
        .then(result => {
            if (result.success === true) {

                const modalSuccess = new bootstrap.Modal(document.getElementById('modal-success'));
                modalSuccess.show();

                document.getElementById('btn-reload').addEventListener('click', function () {
                    window.location.href = "/lista-de-tipos";
                });
            } else {
                const modalDanger = new bootstrap.Modal(document.getElementById('modal-danger'));
                modalDanger.show();
            }
        })
        .catch(error => {
            console.error("Erro:", error);

            const modalDanger = new bootstrap.Modal(document.getElementById('modal-danger'));
            modalDanger.show();
        });
}