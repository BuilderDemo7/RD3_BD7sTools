using System;

namespace Models
{
	
public struct VersionHeader
{
	// the version number in the released game is 2
	uint32	u32VersionNumber;
};

public struct ModelHeader
{
	uint32	u32ModelDataFileOffset;
	uint32	u32FrameDataFileOffset;
	uint32	u32MeshDataFileOffset;
	uint32	u32MaterialDataFileOffset;
};

public struct VertexBufferHeader
{
	uint32				u32VertexBufferNum;
	VertexBufferInfo**	ppVertexBufferInfo;
};

public struct VertexBufferInfo
{
	uint32	u32VerticesNum;
	uint32	u32VertexByteSize;
	uint32	u32VerticesFileOffset;
};

public struct IndexBufferHeader
{
	uint32	u32IndicesNum;
	uint32	u32IndicesFileOffset;
};

public struct LODHeader
{
	uint32		u32LODNum;
	LODInfo**	ppLODInfo;	
};

public struct LODInfo
{
	uint32					u32LODID;
	uint32					u32FramesNum;
	uint32					u32FrameOffset;
	uint32					u32MeshesNum;
	uint32					u32MeshOffset;
	uint32					u32MaterialsNum;
	uint32					u32MaterialOffset;
	uint32					u32IndexStart;
	uint32					u32IndexCount;
	LODVertexBufferInfo**	ppVertexBufferInfo;
};

public struct LODVertexBufferInfo
{
	uint32	u32VertexIndexFirst;
	uint32	u32VertexCount;
};

public struct EffectHeader
{
	uint32	u32EffectsNum;
	uint32	u32EffectDataFileOffset;
};

public struct StringTableHeader
{
	uint32	u32StringsNum;
	uint32	u32StringLength;
	uint32	u32StringTableFileOffset;
};

public struct ModelData
{
	BOOL			boDynamicFlag;
	D3DXVECTOR3		vecMin;
	D3DXVECTOR3		vecMax;
};


public enum BACK_FACE_CULL
{
	BACK_FACE_CULL_ON			= 0,
	BACK_FACE_CULL_OFF			= 1,
	BACK_FACE_CULL_DONT_CARE	= 2,
	BACK_FACE_CULL_FORCE_DWORD	= 0x7fffffff,
};

public enum ALPHA_SORT
{
	ALPHA_SORT_MODEL		= 0,
	ALPHA_SORT_SCENE		= 1,
	ALPHA_SORT_OFF			= 2,
	ALPHA_SORT_FORCE_DWORD	= 0x7fffffff,
};

public struct MeshData
{
	D3DXVECTOR3					vecMin;
	D3DXVECTOR3					vecMax;
	ALPHA_SORT					eAlphaSort;
	BACK_FACE_CULL				eBackFaceCull;
	uint32						u32MaterialsNum;
	uint32*						pu32MaterialIndex;
};

public enum PRIMITIVE_TYPE
{
	PRIMITIVE_TYPE_TRIANGLE_LIST	= 0,
	PRIMITIVE_TYPE_TRIANGLE_STRIP	= 1,
	PRIMITIVE_TYPE_MAX				= 2,
	PRIMITIVE_TYPE_FORCE_DWORD		= 0x7fffffff,
};

public enum TRANSPARENCY
{
	TRANSPARENCY_OFF			= 0,
	TRANSPARENCY_ON				= 1,
	TRANSPARENCY_FORCE_DWORD	= 0x7fffffff,
};

public struct MaterialData
{
	PRIMITIVE_TYPE					ePrimitiveType;
	TRANSPARENCY					eTransparency;
	uint32							u32EffectOffset;
	int32							n32TextureNameStringTableIndex[4];
	uint32							u32VertexBuffer;
	uint32							u32VertexCount;
	uint32							u32VertexBufferOffset;
	uint32							u32IndexCount;
	uint32							u32IndexBufferOffset;
};

public struct FrameData
{ 
	int32			n32NameStringTableIndex;
	D3DXVECTOR3		vecMin;
	D3DXVECTOR3		vecMax;
	D3DXMATRIX		matLocal;
	int32			n32ParentIndex;
	int32			n32NextIndex;
	int32			n32ChildIndex;
	uint32			u32MeshesNum;
	uint32*			pu32MeshIndex;
};

public struct TechniqueData
{
	uint32				u32DeclElementsNum;
	uint32				u32VertexByteSize;
	D3DVERTEXELEMENT9	Decl[MAX_FVF_DECL_SIZE];
};

public struct EffectData
{
	uint32			u32EffectNameStringTableIndex;
	uint32			u32TechniquesNum;
	TechniqueData*	pTechnique;	
};


// C++ code, will be useful
/*
WriteP3D()
{
	m_pFile = fopen( m_szFileName, "wb" );

	fwrite( &m_VersionHeader, 1, sizeof( VersionHeader ), m_pFile );
	fwrite( &m_ModelHeader, 1, sizeof( ModelHeader ), m_pFile );
	fwrite( &m_VertexBufferHeader, 1, sizeof( VertexBufferHeader ), m_pFile );
	fwrite( m_pVertexBufferInfo, m_VertexBufferHeader.u32VertexBufferNum, sizeof( VertexBufferInfo ), m_pFile );
	fwrite( &m_IndexBufferHeader, 1, sizeof( IndexBufferHeader ), m_pFile );
	fwrite( &m_LODHeader, 1, sizeof( LODHeader ), m_pFile );
	for( int32 n32LOD=0; n32LOD<(int32)m_LODHeader.u32LODNum; n32LOD++ )
	{
		fwrite( &m_pLODInfo[n32LOD], 1, sizeof( LODInfo ), m_pFile );
		fwrite( m_ppLODVertexBufferInfo[n32LOD], m_VertexBufferHeader.u32VertexBufferNum, sizeof( LODVertexBufferInfo ), m_pFile );
	}
	fwrite( &m_EffectHeader, 1, sizeof( EffectHeader ), m_pFile );
	fwrite( &m_StringTableHeader, 1, sizeof( StringTableHeader ), m_pFile );

	// Write out the model data.
	fwrite( &m_ModelData, 1, sizeof( ModelData ), m_pFile );
	
	// Write out the frame data.
	for( int32 n32Frame=0; n32Frame<(int32)m_u32FramesTotal; n32Frame++ )
	{
		fwrite( &m_pFrameData[n32Frame], 1, sizeof( FrameData ), m_pFile );
		fwrite( m_pFrameData[n32Frame].pu32MeshIndex, m_pFrameData[n32Frame].u32MeshesNum, sizeof( uint32 ), m_pFile );
	}
	
	// Write out the mesh data.
	for( int32 n32Mesh=0; n32Mesh<(int32)m_u32MeshesTotal; n32Mesh++ )
	{
		fwrite( &m_pMeshData[n32Mesh], 1, sizeof( MeshData ), m_pFile );
        fwrite( m_pMeshData[n32Mesh].pu32MaterialIndex, m_pMeshData[n32Mesh].u32MaterialsNum, sizeof( uint32 ), m_pFile );
	}
	
	// Write out the material data.
	fwrite( m_pMaterialData, m_u32MaterialsTotal, sizeof( MaterialData ), m_pFile );

	// Write out the effect data.
	for( int32 n32Effect=0; n32Effect<(int32)m_EffectHeader.u32EffectsNum; n32Effect++ )
	{
		fwrite( &m_pEffectData[n32Effect], 1, sizeof( EffectData ), m_pFile );
		if( m_pEffectData[n32Effect].u32TechniquesNum )
		{
			fwrite( m_pEffectData[n32Effect].pTechnique, m_pEffectData[n32Effect].u32TechniquesNum, sizeof( TechniqueData ), m_pFile );
		}
	}
	
	// Write out the string table.
	for( int32 n32String=0; n32String<(int32)m_StringTableHeader.u32StringsNum; n32String++ )
	{
		fwrite( m_ppStringTable[n32String], 1, m_StringTableHeader.u32StringLength, m_pFile );
	}

	// Write out the vertex data.
	for( int32 n32VertexBuffer=0; n32VertexBuffer<(int32)m_VertexBufferHeader.u32VertexBufferNum; n32VertexBuffer++ )
	{
		fwrite( m_ppu8VertexData[n32VertexBuffer], m_pVertexBufferInfo[n32VertexBuffer].u32VerticesNum, m_pVertexBufferInfo[n32VertexBuffer].u32VertexByteSize, m_pFile );
	}

	// Write out the index data.
	fwrite( m_pu16IndexData, m_IndexBufferHeader.u32IndicesNum, sizeof( uint16 ), m_pFile );

	// Close the file.
	fclose( m_pFile );
}
*/

}