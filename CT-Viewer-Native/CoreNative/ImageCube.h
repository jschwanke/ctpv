//ImageCube
#include "stdafx.h"

#ifdef UNMANAGED_EXPORTS
   #define UNMANAGED_API __declspec(dllexport)
#else
   #define UNMANAGED_API __declspec(dllimport)
#endif

#ifndef _IMAGECUBE_H_
#define _IMAGECUBE_H_

namespace CoreNative
{
	class UNMANAGED_API ImageCube
	{
		public:
			enum BYTEORDER {LSB,MSB};
			enum IMAGEORIENTATION {Transversal, Sagittal, Frontal};

		private:
			char* imageID;
			ImageCube::BYTEORDER byteOrder;
			ImageCube::IMAGEORIENTATION imageOrientation;
			int dataSize;
			int dimension;
			int dimX;
			int dimY;
			int dimZ;
			char* hedFile;
			char* ctxFile;
			float pixelSize;
			float sliceDistance;
			int offsetX;
			int offsetY;
			short*** cubeData;

		public:
			ImageCube(const char* imageID, const char* hedFile, const char* ctxFile);
			~ImageCube();
			const char* GetImageID(void);
			ImageCube::BYTEORDER GetByteOrder(void);
			ImageCube::IMAGEORIENTATION GetImageOrientation(void);
			const char* GetCreationInfo(void);
			const char* GetCreatedBy(void);
			int GetDataSize(void);
			int GetDimension(void);
			int GetDimX(void);
			int GetDimY(void);
			int GetDimZ(void);
			const char* GetHedFile(void);
			const char* GetCtxFile(void);
			float GetPixelSize(void);
			float GetSliceDistance(void);
			int GetOffsetX(void);
			int GetOffsetY(void);
			short*** GetCubeData();
			void SetByteOrder(ImageCube::BYTEORDER byteOrder);		
			void SetImageOrientation(ImageCube::IMAGEORIENTATION imageOrientation);
			void SetDataSize(int dataSize);
			void SetDimension(int dimension);
			void SetDimX(int dimX);
			void SetDimY(int dimY);
			void SetDimZ(int dimZ);
			void SetPixelSize(float pixelSize);
			void SetSliceDistance(float sliceDistance);
			void SetOffsetX(int offsetX);
			void SetOffsetY(int offsetY);
			void SetCubeData(short*** cubeData);
			float GetBottomLineInMM();
			int GetBottomLineInPixel();
			float GetTopLineInMM();
			int GetTopLineInPixel();
			bool IsPixelDataAvailable();
			short** GetSlice(ImageCube::IMAGEORIENTATION orientation, int sliceIndex);
			int TrafoScreenPosToSlice(int y);
			int TrafoSliceToScreenPos(int slice);
	};
};
#endif