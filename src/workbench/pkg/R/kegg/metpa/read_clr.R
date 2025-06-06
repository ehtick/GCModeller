
#' Create metpa background model from the .NET clr object
#' 
#' @param metpa A GCModeller metpa clr object which is generated 
#'    from the ``background::metpa`` function.
#' 
const .cast_CLR_metpa = function(metpa) {
    return({
        # summary information
        uniq.count: [metpa]::unique_count,

        # pathway network information
          path.ids: .write_pathIds([metpa]::pathIds),        
         path.smps: .write_pathSmps([metpa]::pathSmps),
        graph.list: .write_graphList([metpa]::graphList),

        # molecules cluster information
         mset.list: .write_msetList([metpa]::msetList),
          rbc.list: .write_rbcList([metpa]::rbcList),        
          dgr.list: .write_dgrlist([metpa]::dgrList)        
    });
}

const .write_dgr = function(dgr) {
    const vec = [dgr]::dgr;

    if (!is.null(vec)) {
        names(vec) = [dgr]::kegg_id;
    }

    vec;
}

const .write_graph = function(graph) {

}

const .write_graphList = function(graphList) {
    lapply([graphList]::graphs, g -> .write_graph(graph = g));
}

const .write_dgrlist = function(dgrList) {
    const dgrlist = lapply([dgrList]::pathways, map -> .write_dgr(dgr = map));

    print("view of the pathway network degree data:");
    str(dgrlist);

    dgrlist;
}

const .write_mset = function(mset) {
    const setdata = [mset]::kegg_id;

    if (!is.null(setdata)) {
        names(setdata) = [mset]::metaboliteNames;
    }

    setdata;
}

const .write_msetList = function(msetList) {
    const msetlist = lapply([msetList]::list, map -> .write_mset(mset = map));

    print("view of the pathway compounds list data:");
    str(msetlist);

    msetlist;
}

const .write_pathIds = function(pathIds) {
    const vec = [pathIds]::ids;

    if (!is.null(vec)) {
        names(vec) = [pathIds]::pathwayNames;
    }

    print("view of the pathway id and names data:");
    str(vec);

    vec;
}

const .write_rbc = function(rbc) {
    const vec = [rbc]::data;

    if (!is.null(vec)) {
        names(vec) = [rbc]::kegg_id;
    }

    vec;
}

const .write_rbcList = function(rbcList) {
    const rbclist = lapply([rbcList]::list, map -> .write_rbc(rbc = map));

    print("view of the pathway network relative betweeness data:");
    str(rbclist);

    rbclist;
}

#' write HMDB pathway data
#' 
const .write_pathSmps = function(pathSmps) {
    const vec = [pathSmps]::ids;

    if (!is.null(vec)) {
        names(vec) = sapply([pathSmps]::Smps, idset -> paste(idset, sep = "; "));
    }

    print("View of the pathway names:");
    str(vec);

    vec;
}