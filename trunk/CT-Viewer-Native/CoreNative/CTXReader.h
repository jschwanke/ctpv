//CTXReader.h
#include "stdafx.h"
#include "ImageCube.h"

#ifdef UNMANAGED_EXPORTS
   #define UNMANAGED_API __declspec(dllexport)
#else
   #define UNMANAGED_API __declspec(dllimport)
#endif

#ifndef _CTXReader_H_
#define _CTXReader_H_

namespace CoreNative
{
	class UNMANAGED_API CTXReader
	{
		public:
			CTXReader();
			~CTXReader();
			CoreNative::ImageCube* ReadFiles(const char* hedFile, const char* ctxFile,const char* id);
			
		private:
			void ReadHedFile(ImageCube* imageCube);
			void ReadCtxFile(ImageCube* imageCube);
			void ExtractFileData(ImageCube* imageCube, const char* input);
	};
};
#endif