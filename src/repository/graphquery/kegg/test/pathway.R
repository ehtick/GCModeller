# require(kegg_graphquery);

options(http.cache_dir = `${dirname(@script)}/.cache/`);

 kegg_pathway(
     "D:\GCModeller\src\repository\graphquery\kegg\test\pathway.html"
 )
 |> xml
 |> writeLines(con = `${dirname(@script)}/pathway.XML`)
 ;