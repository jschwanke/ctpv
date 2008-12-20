#include "stdafx.h"
#include "ImageCube.h"
#include <cstring>

CoreNative::ImageCube::ImageCube(const char* hedFile, const char* ctxFile, const char* imageID)
{
	size_t length = 0;

	length = strlen(imageID)+1;
	this->imageID = new char[length];
	strncpy(this->imageID, imageID, length);

	length = strlen(hedFile)+1;
	this->hedFile = new char[length];
	strncpy(this->hedFile, hedFile, length);

	length = strlen(ctxFile)+1;
	this->ctxFile = new char[length];
	strncpy(this->ctxFile, ctxFile, length);
};

CoreNative::ImageCube::~ImageCube()
{
    delete imageID;
	delete hedFile;
	delete ctxFile;

	for(int z=0; z<dimZ; z++)
	{
		for(int y=0; y<dimY; y++)
		{
			delete[] cubeData[z][y];
		}
	}
};

const char* CoreNative::ImageCube::GetImageID(void)
{
	return (const char*) imageID;
};

CoreNative::ImageCube::BYTEORDER CoreNative::ImageCube::GetByteOrder(void)
{
	return byteOrder;
};

CoreNative::ImageCube::IMAGEORIENTATION CoreNative::ImageCube::GetImageOrientation(void)
{
	return imageOrientation;
};

int CoreNative::ImageCube::GetDataSize(void)
{
	return dataSize;
};

int CoreNative::ImageCube::GetDimension(void)
{
	return dimension;
};

int CoreNative::ImageCube::GetDimX(void)
{
	return dimX;
};

int CoreNative::ImageCube::GetDimY(void)
{
	return dimY;
};

int CoreNative::ImageCube::GetDimZ(void)
{
	return dimZ;
};

const char* CoreNative::ImageCube::GetHedFile(void)
{
	return (const char*) hedFile;
};

const char* CoreNative::ImageCube::GetCtxFile(void)
{
	return (const char*) ctxFile;
};

float CoreNative::ImageCube::GetPixelSize(void)
{
	return pixelSize;
};

float CoreNative::ImageCube::GetSliceDistance(void)
{
	return sliceDistance;
};

int CoreNative::ImageCube::GetOffsetX(void)
{
	return offsetX;
};

int CoreNative::ImageCube::GetOffsetY(void)
{
	return offsetY;
};

short*** CoreNative::ImageCube::GetCubeData()
{
	return this->cubeData;
};

void CoreNative::ImageCube::SetByteOrder(CoreNative::ImageCube::BYTEORDER byteOrder)
{
	this->byteOrder = byteOrder;
};

void CoreNative::ImageCube::SetImageOrientation(CoreNative::ImageCube::IMAGEORIENTATION imageOrientation)
{
	this->imageOrientation = imageOrientation;
};

void CoreNative::ImageCube::SetDataSize(int dataSize)
{
	this->dataSize = dataSize;
};

void CoreNative::ImageCube::SetDimension(int dimension)
{
	this->dimension = dimension;
};

void CoreNative::ImageCube::SetDimX(int dimX)
{
	this->dimX = dimX;
};

void CoreNative::ImageCube::SetDimY(int dimY)
{
	this->dimY = dimY;
};

void CoreNative::ImageCube::SetDimZ(int dimZ)
{
	this->dimZ = dimZ;
};

void CoreNative::ImageCube::SetPixelSize(float pixelSize)
{
	this->pixelSize = pixelSize;
};

void CoreNative::ImageCube::SetSliceDistance(float sliceDistance)
{
	this->sliceDistance = sliceDistance;
};

void CoreNative::ImageCube::SetOffsetX(int offsetX)
{
	this->offsetX = offsetX;
};

void CoreNative::ImageCube::SetOffsetY(int offsetY)
{
	this->offsetY = offsetY;
};

void CoreNative::ImageCube::SetCubeData(short*** cubeData)
{
	//if(cubeData != NULL)
	//{
	//	for(int z=0; z<dimZ; z++)
	//	{
	//		for(int y=0; y<dimY; y++)
	//		{
	//			delete[] cubeData[z][y];
	//		}
	//	}
	//}
	this->cubeData = cubeData;
};

float CoreNative::ImageCube::GetBottomLineInMM()
{
	float img_height_mm = dimension * pixelSize;
    float cube_height_mm = dimZ * sliceDistance;
    float start_mm = (img_height_mm + cube_height_mm) / 2.0f;
    return start_mm;
};

int CoreNative::ImageCube::GetBottomLineInPixel()
{
	return ((int)(GetBottomLineInMM() / pixelSize));
};

float CoreNative::ImageCube::GetTopLineInMM()
{
	float img_height_mm = dimension * pixelSize;
    float cube_height_mm = dimZ * sliceDistance;
    float offset_mm = (img_height_mm - cube_height_mm) / 2.0f;
    return offset_mm;
};

int CoreNative::ImageCube::GetTopLineInPixel()
{
	return ((int)(GetTopLineInMM() / pixelSize));
};

bool CoreNative::ImageCube::IsPixelDataAvailable()
{
	bool result = false;
    if (cubeData)
    {
        result = true;
    }
    return result;
};

short** CoreNative::ImageCube::GetSlice(ImageCube::IMAGEORIENTATION orientation, int sliceIndex)
{
	short** slice = NULL;
	int i, j;
	int zIndex = 0;
	float curPos = 0;
	float curPixel = 0;
	float bottomLine = GetBottomLineInMM();
	float topLine = GetTopLineInMM();

	if (IsPixelDataAvailable()) 
	{
		zIndex = dimZ - 1;
		if (orientation == ImageCube::IMAGEORIENTATION::Transversal)
		{
			if ((sliceIndex >= 0) && (sliceIndex < dimZ))
			{
				slice = cubeData[sliceIndex];
			}
		}
		else if (orientation == ImageCube::IMAGEORIENTATION::Frontal) 
		{
			if ((sliceIndex >= 0) && (sliceIndex < dimension)) 
			{
				slice = new short*[dimension];
				curPos = dimZ * sliceDistance;
				for (i = 0; i < dimension; i++) 
				{
					curPixel = ((float) (dimension - i) * pixelSize);
					if ((curPixel >= topLine) && (curPixel <= bottomLine)) 
					{
						if (curPos < ((float) (zIndex) * sliceDistance))
						{
							zIndex--;
						}
						if ((zIndex >= 0) && (zIndex < dimZ)
							&& (sliceIndex >= offsetX)
								&& (sliceIndex < (offsetX + dimX)))
						{
							slice[i] = cubeData[zIndex][sliceIndex];
						}
						else
						{
							slice[i] = new short[dimension];
						}
						curPos = curPos - pixelSize;
					} 
					else
					{
						slice[i] = new short[dimension];
					}
				} 
			}
		}
		else if (orientation == ImageCube::IMAGEORIENTATION::Sagittal) 
		{
			slice = new short*[dimension];
			for (unsigned int pos = 0; pos < dimension; pos++)
			{
				slice[pos] = new short[dimension];
			}
			if ((sliceIndex >= 0) && (sliceIndex < dimension)) 
			{
				curPos = dimZ * sliceDistance;
				for (i = 0; i < dimension; i++) 
				{
					curPixel = ((float) (dimension - i) * pixelSize);
					if ((curPixel >= topLine) && (curPixel <= bottomLine)) 
					{
						if (curPos < ((float) (zIndex) * sliceDistance))
						{
							zIndex--;
						}

						if ((zIndex >= 0) && (zIndex < dimZ)
								&& (sliceIndex >= offsetY)
								&& (sliceIndex < (offsetY + dimY))) 
						{
							for (j = 0; j < dimension; j++)
							{
								slice[i][j] = cubeData[zIndex][j][sliceIndex];
							}
						}
						curPos = curPos - pixelSize;
					}
				}
			}
		}	
	}
	return slice;
};

int CoreNative::ImageCube::TrafoScreenPosToSlice(int y)
{
	int top = GetTopLineInPixel();
    int bottom = GetBottomLineInPixel();
    int slice = 0;

    if (y <= top)
    {
        slice = dimZ - 1;
    }
    else if (y >= bottom)
    {
        slice = 0;
    }
    else
    {
        int dist_in_pix = y - top;
        float dist_in_mm = dist_in_pix * pixelSize;
        slice = (int)(dimZ - (dist_in_mm / sliceDistance));
    }
    return slice;
};

int CoreNative::ImageCube::TrafoSliceToScreenPos(int slice)
{
	float top = GetTopLineInMM();
    int dist_in_slices = dimZ - slice;
    float dist_in_mm = (dist_in_slices - 0.5f) * sliceDistance;
    float offset = (top + dist_in_mm) / pixelSize;
    return ((int)(offset));
};