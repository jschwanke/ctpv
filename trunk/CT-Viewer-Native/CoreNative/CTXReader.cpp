#include "stdafx.h"
#include "CTXReader.h"
#include <iostream>
#include <fstream>
#include <cstring>

CoreNative::CTXReader::CTXReader()
{

}

CoreNative::CTXReader::~CTXReader()
{

}

CoreNative::ImageCube* CoreNative::CTXReader::ReadFiles(const char* hedFile, const char* ctxFile,const char* id)
{
	CoreNative::ImageCube* cube = new CoreNative::ImageCube(hedFile, ctxFile, id);
	ReadHedFile(cube);
	ReadCtxFile(cube);
	return cube;
};

void CoreNative::CTXReader::ReadHedFile(ImageCube* imageCube)
{
	std::fstream stream;
	stream.open(imageCube->GetHedFile(), std::fstream::in);
	char* buffer = new char[255];
	while(stream.getline(buffer,255))
	{
		ExtractFileData(imageCube,buffer);
	}
	stream.close();
};

void CoreNative::CTXReader::ReadCtxFile(ImageCube* imageCube)
{
	std::fstream stream;
	stream.open(imageCube->GetCtxFile(), std::fstream::in|std::fstream::binary);
	stream.seekg (0, std::ifstream::end);
	int length = stream.tellg();
	stream.seekg (0, std::ifstream::beg);

	char* buffer = new char [length];
	stream.read(buffer,length);

	int i = 0;

	short*** cubeData = new short**[imageCube->GetDimZ()];
	for(int z=0; z<imageCube->GetDimZ(); z++)
	{
		cubeData[z] = new short*[imageCube->GetDimY()];
		for(int y=0; y<imageCube->GetDimY(); y++)
		{
			cubeData[z][y] = new short[imageCube->GetDimX()];
			for(int x=0; x<imageCube->GetDimX(); x++)
			{
				char b1 = buffer[i++];
				char b2 = buffer[i++];
				if(imageCube->GetByteOrder() == ImageCube::BYTEORDER::LSB)
				{
					cubeData[z][y][x] = (short) ((b2 & 0xff) << 8 | (b1 & 0xff));
				}
				else
				{
					cubeData[z][y][x] = (short) ((b1 & 0xff) << 8 | (b2 & 0xff));
				}
			}
		}
	}

	stream.close();
	imageCube->SetCubeData(cubeData);
	delete buffer;
};

void CoreNative::CTXReader::ExtractFileData(ImageCube* imageCube, const char* input)
{
	const char* value = strstr(input, " ")+1;
	size_t lengthValue = strlen(value);
	size_t lengthInput = strlen(input)-lengthValue-1;

	if( strncmp(input,"primary_view",lengthInput) == 0 )
	{
		if (strncmp(value,"transversal",lengthValue) == 0)
        {
			imageCube->SetImageOrientation(ImageCube::IMAGEORIENTATION::Transversal);
        }
        else if (strncmp(value,"sagittal",lengthValue) == 0)
        {
			imageCube->SetImageOrientation(ImageCube::IMAGEORIENTATION::Sagittal);
        }
        else if (strncmp(value,"frontal",lengthValue) == 0)
        {
			imageCube->SetImageOrientation(ImageCube::IMAGEORIENTATION::Frontal);
        }
	}
	else if( strncmp(input,"num_bytes",lengthInput) == 0 )
	{
		imageCube->SetDataSize(atoi(value));
	}
	else if( strncmp(input,"byte_order",lengthInput) == 0 )
	{
		if ( (strncmp(input,"vms",lengthInput) == 0) || (strncmp(input,"aix",lengthInput) == 0))
        {
			imageCube->SetByteOrder(ImageCube::BYTEORDER::MSB);
        }
        else
        {
            imageCube->SetByteOrder(ImageCube::BYTEORDER::LSB);
        }
	}
	else if( strncmp(input,"slice_dimension",lengthInput) == 0 )
	{
		imageCube->SetDimension(atoi(value));
	}
	else if( strncmp(input,"pixel_size",lengthInput) == 0 )
	{
		imageCube->SetPixelSize((float) atof(value));
	}
	else if( strncmp(input,"slice_distance",lengthInput) == 0 )
	{
		imageCube->SetSliceDistance((float) atof(value));
	}
	else if( strncmp(input,"xoffset",lengthInput) == 0 )
	{
		imageCube->SetOffsetX(atoi(value));
	}
	else if( strncmp(input,"yoffset",lengthInput) == 0 )
	{
		imageCube->SetOffsetY(atoi(value));
	}
	else if( strncmp(input,"dimx",lengthInput) == 0 )
	{
		imageCube->SetDimX(atoi(value));
	}
	else if( strncmp(input,"dimy",lengthInput) == 0 )
	{
		imageCube->SetDimY(atoi(value));
	}
	else if( strncmp(input,"dimz",lengthInput) == 0 )
	{
		imageCube->SetDimZ(atoi(value));
	}
};