//CubeManager.h
#include "stdafx.h"
#include "CTXReader.h"
#include "ImageCube.h"
#include <vector>

#ifdef UNMANAGED_EXPORTS
   #define UNMANAGED_API __declspec(dllexport)
#else
   #define UNMANAGED_API __declspec(dllimport)
#endif

#ifndef _CUBEMANAGER_H_
#define _CUBEMANAGER_H_

namespace CoreNative
{
	class UNMANAGED_API CubeManager
	{
		private:
			CoreNative::CTXReader* reader;
			std::vector<CoreNative::ImageCube*>* cubes; 

		public:
			CubeManager();
			~CubeManager();
			CoreNative::ImageCube* GetImageCube(const char* imageID);
			std::vector<CoreNative::ImageCube*>* GetAllImageCubes();
			void RemoveImageCube(const char* imageID);
			CoreNative::ImageCube* CreateImageCube(const char* hedFile, const char* ctxFile, const char* id);
			const char* GetImageCubeByFilename(const char* filename);
	};
};
#endif