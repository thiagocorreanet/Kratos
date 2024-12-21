// Função genérica para inicializar tabelas DataTable
function initializeDataTable(selector) {
    $(document).ready(function () {
        $(selector).DataTable({
            dom: '<"d-flex justify-content-between align-items-center"<"d-flex"lB><f>>t<"row"<"col-md-6"i><"col-md-6"p>>',
            buttons: [
                'copy',      // Copiar
                'csv',       // Exportar CSV
                'excel',     // Exportar Excel
                'print'      // Imprimir
            ],
            lengthMenu: [[10, 25, 50, 100], [10, 25, 50, 100]],
            order: [], // Desativa a ordenação automática
            language: {
                search: "Buscar:",
                buttons: {
                    copyTitle: 'Copiado para a área de transferência',
                    copySuccess: {
                        _: '%d linhas copiadas',
                        1: '1 linha copiada'
                    }
                },
                lengthMenu: "Exibir _MENU_ registros por página",
                zeroRecords: "Nenhum registro encontrado",
                info: "Exibindo _START_ a _END_ de _TOTAL_ registros",
                infoEmpty: "Nenhum registro disponível",
                infoFiltered: "(filtrado de _MAX_ registros totais)",
                paginate: {
                    first: "Primeiro",
                    last: "Último",
                    next: "Próximo",
                    previous: "Anterior"
                }
            }
        });
    });
}
