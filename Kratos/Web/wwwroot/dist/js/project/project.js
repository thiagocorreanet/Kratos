function postModalData() {
    const name = document.getElementById("Name").value;

    const data = {
        name: name
    };

    fetch("/projects/add", { 
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
