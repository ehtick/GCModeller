﻿// export R# package module type define for javascript/typescript language
//
//    imports "rawXML" from "vcellkit";
//
// ref=vcellkit.RawXmlKit@vcellkit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

/**
 * the virtual cell raw data
 * 
*/
declare namespace rawXML {
   module entity {
      /**
       * 
       * 
        * @param raw -
        * @param stream module descripting of the stream content to read, should be a list of content type mapping:
        *  list element name could be: "transcriptome", "proteome", "metabolome"
        *  element content type could be: mass_profile, activity, flux_size
        * @param env -
        * 
        * + default value Is ``null``.
      */
      function names(raw: object, stream: any, env?: object): string;
   }
   module frame {
      /**
       * [debug api]
       * 
       * 
        * @param raw -
      */
      function index(raw: object): object;
      /**
       * get a frame matrix for compares between different samples.
       * 
       * 
        * @param raw -
        * @param tick -
        * @param stream -
        * @param env -
        * 
        * + default value Is ``null``.
      */
      function matrix(raw: string, tick: object, stream: any, env?: object): any;
   }
   module open {
      /**
        * @param mode default value Is ``'read'``.
        * @param args default value Is ``null``.
        * @param env default value Is ``null``.
      */
      function vcellPack(file: string, mode?: string, args?: any, env?: object): object|object;
      /**
       * open gcXML raw data file for read/write
       * 
       * 
        * @param mode 
        * + default value Is ``'read'``.
        * @param args -
        * 
        * + default value Is ``null``.
        * @param env -
        * 
        * + default value Is ``null``.
      */
      function vcellXml(file: string, mode?: string, args?: any, env?: object): object|object;
   }
   module time {
      /**
       * Get a sample matrix data in a timeline.
       * 
       * 
        * @param raw -
        * @param stream -
        * 
        * + default value Is ``null``.
        * @param env -
        * 
        * + default value Is ``null``.
      */
      function frames(raw: any, stream?: any, env?: object): object;
   }
}
