#' Read zip stream data
#' 
#' @description this function open the given zip archive 
#'     file and then load the first file as data stream 
#'     object if the zip entry name is missing. 
#' 
#' @param zipfile the file path of the target zip file to 
#'     read data stream
#' @param entryName the zip entry name.
#' 
#' @return A data steam object that read from the 
#'     given zip archive file.
#' 
const .readZipStream = function(zipfile, entryName = NULL) {
    using zip as open.zip(zipfile) {
		const zipfile = as.object(zip);
        const names as string = zipfile$ls;
        const data = zip[[entryName || names[1]]];

        # returns the target data stream
        # object! 
        data;
    }
}

#' helper function for makes the given id set unique 
#' 
#' @param id a character vector of the object id, duplicated 
#'    value may contains inside.
#' 
#' @details this function andalso will removes the id which is in 
#'    empty literal value, example as: empty string, NA string, 
#'    NULL string, etc.
#' 
const _unique_idset = function(id) {
    id = id[id != ""];
    id = id[id != "NULL"];
    id = id[id != "NA"];
    
    unique(id);
}