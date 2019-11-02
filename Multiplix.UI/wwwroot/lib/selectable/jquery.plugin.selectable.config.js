/*
 * Ponto de configuração de listas gerenciadas pelo plugin jquery selectable.
 * 
 * O comportamento padrão do plugin foi alterado para o seguinte:
 * - ao clicar em um item da lista, alternar entre marcar e desmarcar, 
 *   conservando sempre os outros elementos como estão.
 *
 * Parâmetros a passar: 
 *   appendTo
 *   listSelector
 *   listItemSelectedClass
 *   listItemSelectedAppendContent
 *   listItemUnSelectedClass
 *   listItemUnSelectedAppendContent
 *   valuesSelector
 *   totalInfoSelector
 *   totalInfoSelectorProp
 *   totalInfoSelectorTextBase
 */

function jquerySelectableConfig(params) {
    var appendTo = params.appendTo;
    
    var listSelector = params.listSelector;

    var listItemSelectedClass = params.listItemSelectedClass;
    var listItemSelectedAppendContent = params.listItemSelectedAppendContent;
    var listItemUnSelectedClass = params.listItemUnSelectedClass;
    var listItemUnSelectedAppendContent = params.listItemUnselectedAppendContent;
    
    var valuesSelector = params.valuesSelector;
    var totalInfoSelector = params.totalInfoSelector;
    var totalInfoSelectorProp = params.totalInfoSelectorProp;
    var totalInfoSelectorTextBase = params.totalInfoSelectorTextBase;

    $(listSelector).selectable({
        appendTo: appendTo,

        create: function (event, ui) {
            console.log("create:");
            console.log(ui);

            // criando variaveis "dentro" do elemento
            $(listSelector).prop("listSelector", listSelector);

            $(listSelector).prop("listItemSelectedClass", listItemSelectedClass);
            $(listSelector).prop("listItemSelectedAppendContent", listItemSelectedAppendContent);
            $(listSelector).prop("listItemUnSelectedClass", listItemUnSelectedClass);
            $(listSelector).prop("listItemUnSelectedAppendContent", listItemUnSelectedAppendContent);

            $(listSelector).prop("valuesSelector", valuesSelector);
            $(listSelector).prop("totalInfoSelector", totalInfoSelector);
            $(listSelector).prop("totalInfoSelectorProp", totalInfoSelectorProp);
            $(listSelector).prop("totalInfoSelectorTextBase", totalInfoSelectorTextBase);
            $(listSelector).prop("uiJaSelecionados", []);

            updateValuesSelected($(listSelector).prop("listSelector"),
                                 $(listSelector).prop("listItemSelectedClass"),
                                 $(listSelector).prop("listItemUnSelectedClass"),
                                 $(listSelector).prop("valuesSelector"),
                                 $(listSelector).prop("totalInfoSelector"),
                                 $(listSelector).prop("totalInfoSelectorProp"),
                                 $(listSelector).prop("totalInfoSelectorTextBase"));
        },

        unselecting: function (event, ui) { // método chave para mudar o comportamento padrão
            console.log("unselecting:");
            console.log(ui.unselecting.id);

            // quem estava selecionado, continua
            $(ui.unselecting).attr("class", $(listSelector).prop("listItemSelectedClass"));
            $(ui.unselecting).add($(listSelector).prop("listItemSelectedAppendContent"));
            $(listSelector).prop("uiJaSelecionados").push(ui.unselecting);
        },

        selecting: function (event, ui) {
            console.log("selecting:");
            console.log(ui);

            // se já estava na lista dos selecionado, será desmarcado
            $($(listSelector).prop("uiJaSelecionados")).each(function () {
                console.log(this.id + " == " + ui.selecting.id);
                if (this.id == ui.selecting.id) { // já estava selecionado?
                    // desmarca-o
                    $(ui.selecting).attr("class", $(listSelector).prop("listItemUnSelectedClass"));

                    var item = ui.selecting.firstChild;

                    $(ui.selecting).empty();

                    $(ui.selecting).append(item);
                    $(ui.selecting).append($(listSelector).prop("listItemUnSelectedAppendContent"));
                }
            });
        },

        selected: function (event, ui) {
            console.log("selected:");
            console.log(ui);

            $(ui.selected).attr("class", $(listSelector).prop("listItemSelectedClass"));

            var item = ui.selected.firstChild;

            $(ui.selected).empty();

            $(ui.selected).append(item);
            $(ui.selected).append($(listSelector).prop("listItemSelectedAppendContent"));
        },

        stop: function (event, ui) {
            console.log("stop:");
            console.log(ui);
            $(listSelector).prop("uiJaSelecionados", []);
            updateValuesSelected($(listSelector).prop("listSelector"),
                                 $(listSelector).prop("listItemSelectedClass"),
                                 $(listSelector).prop("listItemUnSelectedClass"),
                                 $(listSelector).prop("valuesSelector"),
                                 $(listSelector).prop("totalInfoSelector"),
                                 $(listSelector).prop("totalInfoSelectorProp"),
                                 $(listSelector).prop("totalInfoSelectorTextBase"));
        }
    });
}

function updateValuesSelected(listSelector,
                              listItemSelectedClass,
                              listItemUnSelectedClass,
                              valuesSelector,
                              totalInfoSelector,
                              totalInfoSelectorProp,
                              totalInfoSelectorTextBase) {

    $(valuesSelector).attr('value', '');
    
    // --------------------------------
    
    var itens_selecionados = '';
    var itens_selecionados_qtde = 0;
    var listItemSelectedClass_selector = "";
    $(listItemSelectedClass.split(" ")).each(function () {
        listItemSelectedClass_selector += "." + this;
    })
    console.log(listItemSelectedClass_selector);
    $(listItemSelectedClass_selector, $(listSelector)).each(function () {
        itens_selecionados += this.id + ','
        itens_selecionados_qtde++;
    });
    itens_selecionados = itens_selecionados.substr(0, itens_selecionados.length - 1);
    
    // --------------------------------

    var itens_nao_selecionados_qtde = 0;
    var listItemUnSelectedClass_selector = "";
    $(listItemUnSelectedClass.split(" ")).each(function () {
        listItemUnSelectedClass_selector += "." + this;
    })
    console.log(listItemUnSelectedClass_selector);
    $(listItemUnSelectedClass_selector, $(listSelector)).each(function () {
        itens_nao_selecionados_qtde++;
    });

    var itens_total_qtde = 0;
    var selectorSelectedContainSelectorUnSelected = (listItemSelectedClass_selector.indexOf(listItemUnSelectedClass_selector) > -1);
    if (selectorSelectedContainSelectorUnSelected) {
        itens_total_qtde = itens_nao_selecionados_qtde;
    } else {
        itens_total_qtde = itens_selecionados_qtde + itens_nao_selecionados_qtde;
    }

    // --------------------------------

    $(valuesSelector).attr('value', itens_selecionados);
    $(totalInfoSelector).prop(totalInfoSelectorProp, totalInfoSelectorTextBase + " ( " + itens_selecionados_qtde + " selec. / " + itens_total_qtde + " total )");
}
