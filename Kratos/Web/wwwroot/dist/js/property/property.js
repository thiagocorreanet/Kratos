// Oculta a parte da quantidade de caractere
document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("SelectTypeDataField").addEventListener("change", function () {
        const selectedText = this.options[this.selectedIndex].text.trim().toLowerCase();
        const charCountField = document.getElementById("QuantityField").parentElement.parentElement;

        if (selectedText === "string") {
            charCountField.style.display = "block";
        } else {
            charCountField.style.display = "none";
            document.getElementById("QuantityField").value = "";
        }
    });
});

// Oculta a parte dos relacionamentos
document.addEventListener("DOMContentLoaded", function () {
    const relationRequiredCheckbox = document.getElementById("RelationRequiredField");
    const entityRelField = document.getElementById("SelectEntityFiled").parentElement.parentElement;
    const relationshipTypesField = document.getElementById("RelationshipTypesContainer");

    relationRequiredCheckbox.addEventListener("change", function () {
        if (!this.checked) {
            entityRelField.style.display = "none";
            relationshipTypesField.style.display = "none";
        } else {
            entityRelField.style.display = "block";
            relationshipTypesField.style.display = "block";
        }
    });
});

async function postDataProperty() {
    let id = document.getElementById("Id").value;
    let name = document.getElementById("NameField").value;
    let typeDataId = document.getElementById('SelectTypeDataField').value;
    let isRequired = document.getElementById("RequiredField").checked;
    let quantityCaracter = document.getElementById("QuantityField").value;
    let isRequiredRel = document.getElementById("RelationRequiredField").checked;
    let entityId = document.getElementById('SelectEntityFiled').value;
    let typeRel = document.querySelector('input[name="radios-inline"]:checked')?.parentNode.textContent.trim();

    if (quantityCaracter === "") {
        quantityCaracter = 0
    }

    const data = {
        name: name,
        typeDataId: typeDataId,
        isRequired: isRequired,
        quantityCaracter: quantityCaracter,
        isRequiredRel: isRequiredRel,
        entityId: id,
        typeRel: typeRel,
        entityIdRel: entityId
    };

    try {

        const response = await fetch('/EntitiesProperties/Add', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        });

        if (!response.ok) {
            throw new Error('Erro ao conectar com o servidor.');
        }

        const result = await response.json();

        if (result.success) {
            window.location.href = `/consultar-entidade/${id}`;
        } else {
            showModal('modal-danger', 'btn-reload');
        }
    } catch (error) {
        showModal('modal-danger', 'btn-reload');
        console.error(error);
    }
}

async function deleteDataProperty(id) {
    let entityId = document.getElementById("Id").value;
    
    try {
        const response = await fetch(`/excluir-propriedade/${id}`, {
            method: 'DELETE',
        });

        if (!response.ok) {
            throw new Error('Erro ao conectar com o servidor.');
        }

        const result = await response.json();

        if (result.success) {
            window.location.href = `/consultar-entidade/${entityId}`;
        } else {
            showModal('modal-danger', 'btn-reload');
        }
    } catch (error) {
        console.error('Erro na operação:', error);
        showModal('modal-danger', 'btn-reload');
    }
}


function showModal(modalId, reloadButtonId) {
    const modal = new bootstrap.Modal(document.getElementById(modalId));
    modal.show();

    if (reloadButtonId) {
        document.getElementById(reloadButtonId).addEventListener('click', function () {
            location.reload();
        });
    }
}
