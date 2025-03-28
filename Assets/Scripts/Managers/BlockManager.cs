using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Arkanoid
{
    public class BlockManager : MonoBehaviour
    {
        public static BlockManager Instance { get; private set; }

        [SerializeField] private Block normalBlock;
        [SerializeField] private Block heavyBlock;
        [SerializeField] private Transform blockSpawner;

        private List<Block> blocks = new List<Block>();

        private float spaceBetweenBlocks = 1.5f;
        private float spaceBetweenRows = 0.5f;
        private int maxBlocksInRow = 11;
        [SerializeField] private int numberOfNormalBlocksToGenerate = 50;
        [SerializeField] private int numberOfHeavyBlocksToGenerate = 20;
        private int blocksSpawned = 0;

        private void Awake()
        {
            SetInstance();
            GenerateBlocksPool();
        }

        private void SetInstance()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            Instance = this;
        }

        private void GenerateBlocksPool()
        {
            GenerateBlocks(numberOfNormalBlocksToGenerate, normalBlock);
            GenerateBlocks(numberOfHeavyBlocksToGenerate, heavyBlock);
        }

        private void GenerateBlocks(int numberOfBlocksToGenerate, Block blockType)
        {
            for (int i = 0; i < numberOfBlocksToGenerate; i++)
            {
                InstantiateBlock(blockType);
            }
        }

        private void InstantiateBlock(Block block)
        {
            Block newBlock = Instantiate(block);
            newBlock.transform.SetParent(blockSpawner.transform);
            newBlock.gameObject.SetActive(false);
            blocks.Add(newBlock);
        }

        public void GenerateLevel(int rows, int blocksInRow)
        {
            if (blocksInRow > maxBlocksInRow || blocksInRow % 2 == 0) return;

            MyExtensions.ShuffleList(blocks);

            SpawnColumns(rows, blocksInRow);
        }

        private void SpawnColumns(int rows, int blocksInRow)
        {
            Vector3 spawnCurrentPosition = blockSpawner.transform.position;

            for (int i = 0; i < rows; i++)
            {
                SpawnRow(blocksInRow, ref spawnCurrentPosition);

                SetSpawnPositionXToDefault(ref spawnCurrentPosition.x);

                ChangeSpawnRow(ref spawnCurrentPosition.y);
            }
        }

        private void SetSpawnPositionXToDefault(ref float positionX)
        {
            positionX = blockSpawner.transform.position.x;
        }

        private void SpawnRow(int blocksInRow, ref Vector3 spawnCurrentPosition)
        {
            SearchForBlockInThePool(ref spawnCurrentPosition);
            ChangeSpawnPositionInRow(ref spawnCurrentPosition.x);
            for (int i = 0; i < (blocksInRow - 1) / 2; i++)
            {
                SearchForBlockInThePool(ref spawnCurrentPosition);

                Vector3 opositeBlockPosition = GetOpositeDirectionOfARow(spawnCurrentPosition);

                SearchForBlockInThePool(ref opositeBlockPosition);
                ChangeSpawnPositionInRow(ref spawnCurrentPosition.x);
            }
        }

        private Vector3 GetOpositeDirectionOfARow(Vector3 spawnCurrentPosition)
        {
            spawnCurrentPosition.x *= -1;
            return spawnCurrentPosition;
        }

        private void ChangeSpawnPositionInRow(ref float position)
        {
            position += spaceBetweenBlocks;
        }

        private void ChangeSpawnRow(ref float position)
        {
            position -= spaceBetweenRows;
        }

        private void SearchForBlockInThePool(ref Vector3 position)
        {
            foreach (var block in blocks)
            {
                if (!block.isActiveAndEnabled)
                {
                    SpawnBlock(block,ref position);
                    break;
                }
            }
        }

        private void SpawnBlock(Block block,ref Vector3 position)
        {
            block.transform.position = position;   
            block.gameObject.SetActive(true);
            blocksSpawned++;
        }

        public void ReduceBlockPool()
        {
            blocksSpawned--;

            if (blocksSpawned <= 0)
            {
                LevelManager.Instance.NextLevel();
            }
        }

        public void DespawnBlocks()
        {
            foreach (Block block in blocks)
            {
                if (block.isActiveAndEnabled)
                {
                    block.gameObject.SetActive(false);
                    ReduceBlockPool();
                }
            }
        }
    }
}

