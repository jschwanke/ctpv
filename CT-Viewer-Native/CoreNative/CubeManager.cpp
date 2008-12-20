#include "stdafx.h"
#include "CubeManager.h"
#include <cstring>

CoreNative::CubeManager::CubeManager()
{
	reader = new CoreNative::CTXReader();
	cubes = new std::vector<CoreNative::ImageCube*>;
};

CoreNative::CubeManager::~CubeManager()
{
	delete reader;
	if(!cubes->empty())
	{
		for(unsigned int i=0; i<cubes->size(); i++)
		{
			CoreNative::ImageCube* tempCube = cubes->back();
			cubes->pop_back();
			delete tempCube;
		}
	}
	delete cubes;
};

CoreNative::ImageCube* CoreNative::CubeManager::GetImageCube(const char* imageID)
{
	if(!cubes->empty())
	{
		for(unsigned int i=0; i<cubes->size(); i++)
		{
			CoreNative::ImageCube* tempCube = cubes->at(i);
			if( strncmp(imageID,tempCube->GetImageID(), strlen(imageID)) == 0 )
			{
				return tempCube;
			}
		}
		return NULL;
	}
	else
	{
		return NULL;
	}
};

std::vector<CoreNative::ImageCube*>* CoreNative::CubeManager::GetAllImageCubes()
{
	return cubes;
};

void CoreNative::CubeManager::RemoveImageCube(const char* imageID)
{
	if(!cubes->empty())
	{
		for( std::vector<CoreNative::ImageCube*>::iterator iterator = cubes->begin();
			iterator!= cubes->end(); iterator++)
		{
			if( strncmp(imageID,(*iterator)->GetImageID(), strlen(imageID)) == 0 )
			{
				cubes->erase(iterator);
				break;
			}
		}
	}
};

CoreNative::ImageCube* CoreNative::CubeManager::CreateImageCube(const char* hedFile, const char* ctxFile, const char* id)
{
	CoreNative::ImageCube* cube = reader->ReadFiles(hedFile,ctxFile,id);
	cubes->push_back(cube);
	return cube;
};

const char* CoreNative::CubeManager::GetImageCubeByFilename(const char* filename)
{
	if(!cubes->empty())
	{
		for(unsigned int i=0; i<cubes->size(); i++)
		{
			CoreNative::ImageCube* tempCube = cubes->at(i);
			if( strncmp(filename,tempCube->GetHedFile(), strlen(filename)) == 0 )
			{
				return tempCube->GetImageID();
			}
		}
		return NULL;
	}
	else
	{
		return NULL;
	}
};